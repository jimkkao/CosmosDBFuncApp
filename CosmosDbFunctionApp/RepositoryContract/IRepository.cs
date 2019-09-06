using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryContract
{
    public interface IRepository<T>
    {
        Task<T> Insert(T item);

        Task<List<T>> Get(Expression<Func<T, bool>> func);

        Task<T> Update(T item);

        Task Delete(string uniqueId);
            
    }
}
