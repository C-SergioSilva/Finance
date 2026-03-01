
using Finance.Domain.BaseEntity;

namespace Fincance.Dominio.Interfaces
{
    public interface IBaseRepository<T> where T : BaseGuid
    {
        // Métodos de Leitura
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> Queryable(); // para filtros personalizados

        // Métodos de Escrita
        void Add(T entity);
        void Update(T entity); // Importante para quando houver alteração de dados

        // No caso, o StatusDeleted é um "Soft Delete"
        void StatusDeleted(T entity);

        // Persistência (Padrão Unit of Work)
        Task<int> SaveChangesAsync();

        Task MarkDeleted(T item);

    }
}
