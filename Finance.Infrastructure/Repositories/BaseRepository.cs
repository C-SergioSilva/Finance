using Fincance.Dominio.Interfaces;
using Finance.Domain.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Fincance.Infrastructure.Context;

namespace Fincance.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseGuid
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // Atalho para a tabela específica
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.SingleOrDefaultAsync(g => g.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void StatusDeleted(T entity)
        {
            // Em vez de remover do banco, apenas mudamos o status
            entity.Deleted = true;
             _dbSet.Update(entity);
        }

        public async Task MarkDeleted(T item)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}