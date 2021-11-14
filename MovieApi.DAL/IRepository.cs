using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieApi.DAL
{
    public interface IRepository<T> where T: class
    {
        Task Add(T Item);
        Task AddAll(IEnumerable<T> Items);
        Task Delete(Guid PrimaryKey);
        Task<List<T>> GetAll();
        Task<T> GetByID(Guid Id);
        Task<T> GetByID(int Id);
        Task<List<T>> Get<T2>(Expression<Func<T, bool>> predicate);
        Task Save(T Item);
        Task SaveAll(IEnumerable<T> Items);

    }
}