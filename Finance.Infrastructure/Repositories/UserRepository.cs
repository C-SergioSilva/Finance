using Fincance.Infrastructure.Context;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Fincance.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context){}

        public async Task<User> GetByEmail(string email)
        {
            // O .AsNoTracking() é uma dica de performance: como é para login/consulta, 
            // não precisamos que o EF "monitore" esse objeto.
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
