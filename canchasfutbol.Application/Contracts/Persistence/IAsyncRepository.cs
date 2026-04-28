using canchasfutbol.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
            string includeString = null!,
            bool disabledTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
           List<Expression<Func<T, object>>> includes =null!,
           bool disabledTracking = true);

        Task<T> GetByIdAsync(int id); 

        Task<T> GetByGuidAsync(Guid id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        void AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);    

    }
}
