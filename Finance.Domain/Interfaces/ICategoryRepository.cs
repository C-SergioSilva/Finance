using Fincance.Dominio.Interfaces;
using Finance.Domain.Models;

namespace Finance.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // Buscar apenas as categorias de um usuário (ex: Aluguel, Salário)
        Task<IEnumerable<Category>> GetByUser(Guid userId);
    }
}
