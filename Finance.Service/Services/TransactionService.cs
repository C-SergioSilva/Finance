using AutoMapper;
using Finance.Domain.Enums;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;

namespace Finance.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository,
                                  ICategoryRepository categoryRepository,
                                  IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<TransactionVO> Create(TransactionVO transactionVO)
        {
            // 1. Validação básica de valor
            if (transactionVO.Amount <= 0)
                throw new Exception("O valor da transação deve ser maior que zero.");

            // 2. Validação de Segurança: A categoria informada existe e é do usuário logado?
            // Agora 'transactionVO.CategoryId' é Guid, então o GetById funciona perfeitamente!
            var category = await _categoryRepository.GetById(transactionVO.CategoryId);

            if (category == null)
                throw new Exception("Categoria não encontrada.");

            if (category.UserId != transactionVO.UserId)
                throw new Exception("Você não tem permissão para usar esta categoria.");

            // 3. Mapeamento para Entidade
            var entity = _mapper.Map<Transaction>(transactionVO);

            // Garantimos que a data seja a atual caso não venha do front
            if (entity.Date == DateTime.MinValue)
                entity.Date = DateTime.UtcNow;

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TransactionVO>(entity);
        }

        public async Task<IEnumerable<TransactionVO>> GetByUser(Guid userId)
        {
            var transactions = await _repository.GetByUser(userId);
            return _mapper.Map<IEnumerable<TransactionVO>>(transactions);
        }

        public async Task<decimal> GetBalance(Guid userId)
        {
            // O repository já deve estar usando .Include(t => t.Category)
            var transactions = await _repository.GetByUser(userId);

            // 1. Filtramos as transações onde a CATEGORIA é do tipo 1 (Receita)
            var income = transactions
                .Where(t => t.Category.TypeCategory == (int)CategoryType.Income)
                .Sum(t => t.Amount);


            // No código:
            // .Where(t => t.Category.TypeCategory == (int)CategoryType.Income)

            // 2. Filtramos as transações onde a CATEGORIA é do tipo 2 (Despesa)
            var expense = transactions
                .Where(t => t.Category.TypeCategory == (int)CategoryType.Expense)
                .Sum(t => t.Amount);

            // Saldo = Entradas - Saídas
            return income - expense;
        }

        public async Task Delete(Guid id)
        {
            var transaction = await _repository.GetById(id);
            if (transaction == null) throw new Exception("Transação não encontrada.");

            _repository.StatusDeleted(transaction);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(TransactionVO transactionVO)
        {
            // 1. Buscamos a transação existente no banco
            var existingTransaction = await _repository.GetById(transactionVO.Id);

            if (existingTransaction == null)
                throw new Exception("Transação não encontrada para atualização.");

            // 2. Validação de Segurança: Garante que o usuário não está editando algo de outro
            if (existingTransaction.UserId != transactionVO.UserId)
                throw new Exception("Você não tem permissão para alterar esta transação.");

            // 3. Validação da Categoria (Caso o usuário tenha mudado a categoria no Edit)
            var category = await _categoryRepository.GetById(transactionVO.CategoryId);
            if (category == null || category.UserId != transactionVO.UserId)
                throw new Exception("Categoria inválida ou não pertence ao usuário.");

            // 4. Mapeamos as propriedades do VO para a Entidade que já existe (Rastreamento do EF)
            _mapper.Map(transactionVO, existingTransaction);

            // 5. Persistimos no banco
            _repository.Update(existingTransaction);
            await _repository.SaveChangesAsync();
        }

        public async Task<TransactionVO> GetById(Guid id)
        {
            // Buscamos no repositório (que deve usar .Include(t => t.Category))
            var transaction = await _repository.GetByIdWithCategory(id);

            if (transaction == null)
                throw new Exception("Transação não encontrada.");

            // Mapeamos a Entidade para o VO para enviar ao Front-end
            return _mapper.Map<TransactionVO>(transaction);
        }

    }
}