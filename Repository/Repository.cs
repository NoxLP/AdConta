using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace AdConta.Models
{
    public class Repository<T> : iRepository<T> where T : class
    {
        public Repository()
        {
            this._Objects = new Dictionary<int, WeakReference<T>>();
        }

        #region fields
        private Dictionary<int, WeakReference<T>> _Objects;
        #endregion

        #region properties
        #endregion

        #region helpers
        #endregion

        #region public methods
        public virtual bool TryGet(int id, out T objModel)
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
        /// <summary>
        /// If object model with id doesn't exist, return null, otherwise return database or new object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            T objModel;
            TryGet(id, out objModel);
            return objModel;
        }
        public virtual IEnumerable<T> GetAll()
        {
            this._Objects.
                Select(kvp =>
                {
                    T objModel;
                    if (!kvp.Value.TryGetTarget(out objModel))
                        this._Objects.Remove(kvp.Key);
                    return objModel;
                }).
                Where(x => x != null);
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {

        }

        public virtual void Add(T objModel)
        {

        }
        public virtual void AddRange(IEnumerable<T> objModels)
        {

        }

        public virtual void Remove(T objModel)
        {

        }
        public virtual void RemoveRange(IEnumerable<T> objModels)
        {

        }
        #endregion
    }
}
