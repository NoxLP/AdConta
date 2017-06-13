using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.ViewModel;
using QBuilder;

namespace Repository
{
    public class PropietarioRepository : aRepositoryBase, iRepository
    {
        public PropietarioRepository()
        {

        }

        public ConcurrentDictionary<aVMTabBase, List<Tuple<QueryBuilder, IConditionToCommit>>> Transactions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task ApplyChangesAsync(aVMTabBase VM)
        {
            throw new NotImplementedException();
        }

        public Type GetObjModelType()
        {
            throw new NotImplementedException();
        }

        public void NewVM(aVMTabBase VM)
        {
            throw new NotImplementedException();
        }

        public void RemoveVMTabReferences(aVMTabBase VM)
        {
            throw new NotImplementedException();
        }

        public Task RollbackRepoAsync(aVMTabBase VM)
        {
            throw new NotImplementedException();
        }

        protected override QueryBuilder GetSelectSQL(int id)
        {
            throw new NotImplementedException();
        }

        #region fields
        #endregion

        #region properties
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
