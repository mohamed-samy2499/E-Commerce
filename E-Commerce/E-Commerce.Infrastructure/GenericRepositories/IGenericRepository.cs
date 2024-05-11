using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null,
                                        Expression<Func<T, bool>>[]? filters = null);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> CreateAsync(T entity, string loggedInUserId);
        Task<string> DeleteAsync(int id, string loggedInUserId);

        Task<T> UpdateAsync(T entity, string loggedInUserId);
    }
}
