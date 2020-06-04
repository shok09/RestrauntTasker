using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<T> where T : BaseEntity<int>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> criteria);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
