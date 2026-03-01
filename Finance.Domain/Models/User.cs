using Finance.Domain.BaseEntity;

namespace Finance.Domain.Models
{
    public class User : BaseGuid
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Propriedades de Navegação
        // Um usuário pode ter muitas categorias e muitas transações
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        // Construtor para inicializar as listas e evitar erros de "null reference"
        public User()
        {
            Categories = new List<Category>();
            Transactions = new List<Transaction>();
        }
    }
}
