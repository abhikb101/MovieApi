using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.DAL
{
    public class DALRepository<T> : IRepository<T> where T : class
    {
        protected MovieDBContext _context;
        public DALRepository(MovieDBContext context)
        {
            _context = context;
        }

        public virtual async Task Add(T Item)
        {
            _context.Add(Item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddAll(IEnumerable<T> Items)
        {
            _context.AddRange(Items);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(Guid Id)
        {
            _context.Remove(await GetByID(Id));
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> Get<T2>(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByID(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public virtual async Task<T> GetByID(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public virtual async Task Save(T Item)
        {
            _context.Update(Item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task SaveAll(IEnumerable<T> Items)
        {
            _context.UpdateRange(Items);
            await _context.SaveChangesAsync();
        }
    }
}
