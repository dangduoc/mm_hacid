using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Application.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T t);
        int Count();
        Task<int> CountAsync([Optional] Expression<Func<T, bool>> match);
        void Delete(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn(params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsyn();
        Task<T> GetAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string id);
        Task<T> GetAsync(Guid id);
        void Save();
        Task<int> SaveAsync(CancellationToken cancellation);
        void Update(T t);
    }
}
