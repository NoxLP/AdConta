using AdConta.ViewModel;
using MQBStatic;
using QBuilder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class aRepositoryBase : MQBStatic_QBuilder, IDisposable
    {
        public aRepositoryBase()
        {
        }

        #region fields
        protected SemaphoreSlim _RepoSphr = new SemaphoreSlim(1, 1);
        protected readonly string _strCon = GlobalSettings.Properties.Settings.Default.conta1ConnectionString;
        
        protected HashSet<int> _Modified;
        protected ConcurrentDictionary<aVMTabBase, Dictionary<int, string[]>> _DirtyMembers = new ConcurrentDictionary<aVMTabBase, Dictionary<int, string[]>>();
        #endregion

        #region helpers
        protected abstract QueryBuilder GetSelectSQL(int id);
        protected virtual QueryBuilder GetUpdateSQL(int id, aVMTabBase VM) { throw new NotImplementedException(); }
        #endregion

        #region public methods
        public bool GetIsBeenModifiedByThisUser(int id)
        {
            return this._Modified.Contains(id);
        }
        public void SetIsBeenModifiedByThisUser(int id)
        {
            this._Modified.Add(id);
        }
        public async Task<bool> TrySetDirtyMember(aVMTabBase VM, int id, string name)
        {
            if (!GetIsBeenModifiedByThisUser(id)) return false;

            await this._RepoSphr.WaitAsync();

            if (!this._DirtyMembers[VM].ContainsKey(id)) this._DirtyMembers[VM].Add(id, new string[] { name });
            else this._DirtyMembers[VM][id] = this._DirtyMembers[VM][id].Union(new string[1] { name }).ToArray();

            this._RepoSphr.Release();
            return true;
        }
        #endregion

        #region Dispose
        public virtual void Dispose()
        {
            if (this._RepoSphr != null)
            {
                this._RepoSphr.Dispose();
                this._RepoSphr = null;
            }
        }
        #endregion
    }

}
