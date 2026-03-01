using Finance.Domain.Models;

namespace Finance.Service.EntitiesVO
{
    public class UserVO
    {
        public Guid IdUser { get; set; } = Guid.NewGuid(); // Já inicia um novo GUID
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Propriedades de Navegação
        // Um usuário pode ter muitas categorias e muitas transações
        public virtual ICollection<CategoryVO> Categories { get; set; }
        public virtual ICollection<TransactionVO> Transactions { get; set; }

        // Construtor para inicializar as listas e evitar erros de "null reference"
        public UserVO()
        {
            Categories = new List<CategoryVO>(); 
            Transactions = new List<TransactionVO>();
        }
    } 
}
