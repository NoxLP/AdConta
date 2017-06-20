using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    /// <summary>
    /// Object for search list with all object's flat data.
    /// </summary>
    public interface IDataListObject
    {
    }
    /// <summary>
    /// Used only for repositories of objects with a search list.
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface IDataListRepository<Q> where Q : class, IObjModelBase, IDataListObject
    {
        IEnumerable<Q> GetDLOs(IEnumerable<int> OwnerIds);
    }

    /// <summary>
    /// Used only for objects with a search list.
    /// </summary>
    /// <typeparam name="T">DLO object type</typeparam>
    public interface IObjWithDLO<T> where T : IDataListObject
    {
        T GetDLO();
    }
    /*/// <summary>
    /// Used only for repositories of objects with a search list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface iRepoOfObjWithDTO<T>
    {
        T GetDTO(int id);
        IEnumerable<T> GetAllDTO();
    }*/
}
