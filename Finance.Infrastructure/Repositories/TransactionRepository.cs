using Fincance.Infrastructure.Context;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Fincance.Infrastructure.Repositories
{
    // Note que herdamos da Base e implementamos a Interface específica
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context) {}

        // Aqui implementamos o método que a Base não conhece
        public async Task<IEnumerable<Transaction>> GetByPeriod(Guid userId, DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(t => t.UserId == userId && t.Date >= start && t.Date <= end)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        // Cálculo de saldo: Soma de Receitas (Type 1) - Soma de Despesas (Type 0)
        public async Task<decimal> GetBalance(Guid userId)
        {
            var transactions = await _dbSet
                .Where(t => t.UserId == userId)
                .Include(t => t.Category) // Precisamos da categoria para saber o tipo
                .ToListAsync();

            var income = transactions.Where(t => t.Category.TypeCategory == 1).Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Category.TypeCategory == 0).Sum(t => t.Amount);

            return income - expense;
        }
    }
}