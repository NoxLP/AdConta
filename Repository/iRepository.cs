using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using AdConta;

namespace AdConta.Models
{
    public interface iRepository<T> where T : class, iObjModelBase
    {
        T Get(int id);
        IEnumerable<T> GetAllCached();
        IEnumerable<T> FindCached(Func<T, bool> predicate);

        bool Add(T objModel);
        ErrorTryingDBRange AddRange(IEnumerable<T> objModels);
        bool Update(T objModel);
        bool Remove(T objModel);
        ErrorTryingDBRange RemoveRange(IEnumerable<T> objModels);
    }

    public abstract class aRepository<T,R> : iRepository<T> where T : class, iObjModelBase  where R : class, iDapperWrapper<T>
    {
        public aRepository()
        {
            this._Objects = new Dictionary<int, WeakReference<T>>();
        }

        #region fields
        private Dictionary<int, WeakReference<T>> _Objects;
        #endregion

        #region properties
        public abstract R DBWrapper { get; protected set; }
        #endregion

        #region helpers
        public virtual bool TryGetObjModelFromDictionary(int id, out T objModel)
        {
            WeakReference<T> reference;
            if (this._Objects.TryGetValue(id, out reference))
            {
                if (!reference.TryGetTarget(out objModel))
                {
                    this._Objects.Remove(id);
                    return false;
                }
                return true;
            }

            objModel = null;
            return false;
        }
        #endregion

        #region public methods        
        /// <summary>
        /// Look for object model with id at cache, if it doesn't exist, look at DB. If it exists at DB, add to cache and return object. Otherwise return null;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            T objModel;

            if (!TryGetObjModelFromDictionary(id, out objModel))
            {
                objModel = this.DBWrapper.Get(id);

                if (objModel != null)
                    this._Objects.Add(objModel.Id, new WeakReference<T>(objModel));
            }

            return objModel;
        }
        public virtual IEnumerable<T> GetAllCached()
        {
            return this._Objects
                .Select(kvp =>
                {
                    T objModel = null;
                    if (!kvp.Value.TryGetTarget(out objModel))
                        this._Objects.Remove(kvp.Key);
                    return objModel;
                })
                .Where(x => x != null);
        }
        public virtual IEnumerable<T> FindCached(Func<T, bool> predicate)
        {
            return this._Objects
                .Select(kvp =>
                {
                    T objModel = null;

                    if (!kvp.Value.TryGetTarget(out objModel))
                        this._Objects.Remove(kvp.Key);
                    else if (!predicate(objModel))
                        objModel = null;

                    return objModel;
                })
                .Where(x => x != null);
        }
        /// <summary>
        /// Add to cache and DB
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public virtual bool Add(T objModel)
        {
            if (!this.DBWrapper.Create(objModel)) return false;
            if (this._Objects.ContainsKey(objModel.Id)) return false;

            this._Objects.Add(objModel.Id, new WeakReference<T>(objModel));
            return true;
        }
        /// <summary>
        /// Add to cache and DB
        /// </summary>
        /// <param name="objModels"></param>
        /// <returns></returns>
        public virtual ErrorTryingDBRange AddRange(IEnumerable<T> objModels)
        {
            ErrorTryingDBRange DBError = this.DBWrapper.CreateRange(objModels) ? ErrorTryingDBRange.None : ErrorTryingDBRange.DB_ObjectsEnumerableError;
            if (DBError != ErrorTryingDBRange.None) return DBError;

            foreach (T objModel in objModels)
            {
                if (this._Objects.ContainsKey(objModel.Id))
                    return ErrorTryingDBRange.Repo_ObjectsEnumerableError;
            }

            this._Objects.Union(
                objModels.ToDictionary(
                    x => x.Id,
                    x => new WeakReference<T>(x)
                    ));

            return ErrorTryingDBRange.None;
        }
        /// <summary>
        /// Update DB
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public virtual bool Update(T objModel)
        {
            return this.DBWrapper.Update(objModel);
        }
        /// <summary>
        /// Remove from DB
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public virtual bool Remove(T objModel)
        {
            return this.DBWrapper.Remove(objModel);
        }
        /// <summary>
        /// Remove from DB
        /// </summary>
        /// <param name="objModels"></param>
        /// <returns></returns>
        public virtual ErrorTryingDBRange RemoveRange(IEnumerable<T> objModels)
        {
            return this.DBWrapper.RemoveRange(objModels) ? ErrorTryingDBRange.None : ErrorTryingDBRange.DB_ObjectsEnumerableError;
        }
        #endregion
    }
}
