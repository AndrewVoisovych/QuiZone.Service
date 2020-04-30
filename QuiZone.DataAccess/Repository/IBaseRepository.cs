using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuiZone.DataAccess.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        T UpdateWithIgnoreProperty<TProperty>(T entity,
            Expression<Func<T, TProperty>> ignorePropertyExpression);
        Task<T> RemoveByIdAsync(int id);

        IQueryable<T> GetQueryable();

    }
}
