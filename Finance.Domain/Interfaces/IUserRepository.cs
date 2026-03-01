using Fincance.Dominio.Interfaces;
using Finance.Domain.Models;

namespace Finance.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        // Exemplo: Buscar por e-mail para validar login
        Task<User> GetByEmail(string email);
    }
}
