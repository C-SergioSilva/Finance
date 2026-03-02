using Finance.Service.EntitiesVO;

namespace Finance.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionVO> Create(TransactionVO transactionVO);
        Task Update(TransactionVO transactionVO);
        Task Delete(Guid id);
        Task<TransactionVO> GetById(Guid id);
        Task<IEnumerable<TransactionVO>> GetByUser(Guid userId);
        Task<decimal> GetBalance(Guid userId); // O saldo total!
    }
}