using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using AdConta.Models;
using AdConta.ViewModel;
using Extensions;
using QBuilder;
using System.Dynamic;
using Dapper;

namespace Repository
{
    public interface iUnitOfWork
    {
        bool RollbackAllIfRollback { get; set; }
        ConditionsToCommitSQL ConditionsToCommit { get; }

        Task<bool> CommitAsync();
        Task RollbackAsync();
        void RemoveVMTabReferencesFromRepos();
    }

    public class UnitOfWork : iUnitOfWork
    {
        public UnitOfWork(IEnumerable<iRepository> repositories, aVMTabBase tab, bool rollbackAllIfRollback = false)
        {
            this.RollbackAllIfRollback = rollbackAllIfRollback;
            this._Repositories = repositories;
            this._Tab = tab;
            InitRepoTransactions();
        }

        #region fields
        protected readonly string _strCon = GlobalSettings.Properties.Settings.Default.conta1ConnectionString;
        private IEnumerable<iRepository> _Repositories;
        private aVMTabBase _Tab;
        private IDictionary<string,object> _Values;
        private ConditionsToCommitSQL _ConditionsToCommit = new ConditionsToCommitSQL();
        #endregion

        #region properties
        public bool RollbackAllIfRollback { get; set; }
        public ConditionsToCommitSQL ConditionsToCommit { get { return this._ConditionsToCommit; } }
        #endregion

        #region helpers
        private void InitRepoTransactions() { Parallel.ForEach(this._Repositories, repo => repo.NewVM(this._Tab)); }
        private string PrepareTransaction()
        {
            string SQL = $"START TRANSACTION;{Environment.NewLine}";
            
            foreach (iRepository repo in this._Repositories)
            {
                List<Tuple<QueryBuilder, IConditionToCommit>> tuples = repo.Transactions[this._Tab];
                foreach(Tuple<QueryBuilder, IConditionToCommit> tuple in tuples)
                {
                    this._Values = this._Values
                        .Union(tuple.Item1 as IDictionary<string, object>)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    SQL = SQL.Append(tuple.Item1.Query);
                    this._ConditionsToCommit.Add(tuple.Item2);
                }
            }
            
            return SQL;
        }
        #endregion

        #region public methods
        public void RemoveVMTabReferencesFromRepos() { Parallel.ForEach(this._Repositories, repo => repo.RemoveVMTabReferences(this._Tab)); }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RollbackAsync()
        {
            Parallel.ForEach(this._Repositories, repo => repo.RollbackRepoAsync(this._Tab).Forget().ConfigureAwait(false));
            this._ConditionsToCommit.Clear();
            this._Values.Clear();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task<bool> CommitAsync()
        {
            string transaction = await Task.Run(() => PrepareTransaction()).ConfigureAwait(false);
            bool commit;

            using (SqlConnection con = new SqlConnection(this._strCon))
            {
                await con.OpenAsync().ConfigureAwait(false);

                var result = await con.QueryMultipleAsync(transaction, this._Values as ExpandoObject).ConfigureAwait(false);
                commit = this._ConditionsToCommit.GetIfMatchAllConditions(result);

                if (!commit)
                    await con.ExecuteAsync("ROLLBACK;").ConfigureAwait(false);
                else
                    await con.ExecuteAsync("COMMIT;").ConfigureAwait(false);

                con.Close();
            }

            if (!commit)
            {
                if(this.RollbackAllIfRollback)
#pragma warning disable CS4014
                    RollbackAsync().Forget().ConfigureAwait(false);
#pragma warning restore CS4014
                return false;
            }
            else
            {
                //using (SqlConnection con = new SqlConnection(this._strCon))
                //{
                //    await con.OpenAsync().ConfigureAwait(false);
                //    await con.ExecuteAsync("COMMIT;").ConfigureAwait(false);
                //    con.Close();
                //}

                Parallel.ForEach(this._Repositories, repo => repo.ApplyChangesAsync(this._Tab).Forget().ConfigureAwait(false));
                this._ConditionsToCommit.Clear();
                this._Values.Clear();
                return true;
            }
        }
        #endregion
    }

}
