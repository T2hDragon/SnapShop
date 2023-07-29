using Microsoft.EntityFrameworkCore;
using SnapShop.Data;
using SnapShop.Models;

namespace SnapShop.Framework.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _dbContext;

        public EntityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<T> getQuery()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await getQuery().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await getQuery().ToListAsync();
        }

        public async Task Create(T entity)
        {
            await getQuery().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            getQuery().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return;
            }
            getQuery().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
