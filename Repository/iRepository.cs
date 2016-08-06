using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace AdConta.Models
{
    public interface iRepository<T>
    {
        bool TryGet(int id, out T objModel);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T objModel);
        void AddRange(IEnumerable<T> objModels);

        void Remove(T objModel);
        void RemoveRange(IEnumerable<T> objModels);
    }
}
