using DLL.Context;
using DLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly MarketShoesContext _context;

        public BaseRepository(MarketShoesContext context)
        {
            _context = context;
        }
        private DbSet<T> entities;

        protected DbSet<T> Entities => this.entities ??= _context.Set<T>();

        public virtual async Task<T?> CreateAsync(T entity)
        {
            try
            {
                var entry = await Entities.AddAsync(entity).ConfigureAwait(false);
                await this.SaveChangesAsync();
                return entry.Entity;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> FindByConditionalAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities.FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
