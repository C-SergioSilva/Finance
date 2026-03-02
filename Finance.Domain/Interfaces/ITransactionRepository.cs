using Fincance.Dominio.Interfaces;
using Finance.Domain.Models;


namespace Finance.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        // Buscar transações por período e usuário
        Task<IEnumerable<Transaction>> GetByPeriod(Guid userId, DateTime start, DateTime end);

        // Buscar o saldo total (Receitas - Despesas)
        Task<decimal> GetBalance(Guid userId);

        // Adicione esta linha:
        Task<IEnumerable<Transaction>> GetByUser(Guid userId);

        Task<Transaction> GetByIdWithCategory(Guid id); 
    }
}
