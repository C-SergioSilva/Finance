using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Fincance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fincance.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) {}

        public async Task<IEnumerable<Category>> GetByUser(Guid userId)
        {
            return await _dbSet
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}