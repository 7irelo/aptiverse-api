using Aptiverse.Api.Domain.Repositories;
using Aptiverse.Api.Infrastructure.Data;
using Aptiverse.Api.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aptiverse.Api.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T?> GetOneAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetManyAsync(Dictionary<string, object>? filters = null)
        {
            var predicate = QueryFilterBuilder<T>.BuildPredicateFromFilters(filters);
            return await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<T> UpdateAsync(long id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id) ?? throw new Exception("Entity Not Found");
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            await _context.Set<T>().Where(x => EF.Property<long>(x, "Id") == id).ExecuteDeleteAsync();
        }
    }
}
