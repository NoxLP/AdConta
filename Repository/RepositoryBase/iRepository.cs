using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using AdConta.ViewModel;
using System.ComponentModel;
using System.Collections.Concurrent;
using QBuilder;

namespace Repository
{
    public interface IRepository : IDisposable
    {
        ConcurrentDictionary<aVMTabBase, List<Tuple<QueryBuilder, IConditionToCommit>>> Transactions { get; }

        Type GetObjModelType();
    }

    internal interface IRepositoryInternal : IRepository
    {
        void NewVM(aVMTabBase VM);
        Task RemoveVMTabReferences(aVMTabBase VM);
        Task ApplyChangesAsync(aVMTabBase VM);
        Task RollbackRepoAsync(aVMTabBase VM);
    }

    public interface IRepositoryOwnerCdad
    {
        int CurrentSingleOwner { get; }

        SQLCondition GetCurrentOwnerCondition(string condition = "=", string alias = "", string paramAlias = "");
    }

    public interface IRepositoryOwnerCdadEjer
    {
        int CurrentCdadOwner { get; }
        int CurrentEjerOwner { get; }

        IEnumerable<SQLCondition> GetCurrentOwnersCondition(string condition = "=", string separator = ",", string tableAlias = "", string paramAlias = "");
    }
    /// <summary>
    /// T is the dependency, NOT the repository type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryDependent<T> where T : IObjModelBase
    {
        ConcurrentDictionary<T, List<int>> DependenciesDict { get; }
        
        QueryBuilder GetAllDependentByMasterSelectSQL(int dependentIdCodigo);
        QueryBuilder GetIdsDependentByMasterSelectSQL(int dependentIdCodigo);
        QueryBuilder GetDependentByMasterSelectSQL(int dependentIdCodigo, IEnumerable<int> ids);
        Task RollbackDependentAsync(IEnumerable<int> ids);
        Task ApplyDependentAsync(IEnumerable<int> idsToAdd, IEnumerable<int> idsToRemove);
    }
}
