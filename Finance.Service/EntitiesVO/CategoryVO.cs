using Finance.Domain.Models;

namespace Finance.Service.EntitiesVO
{
    public class CategoryVO
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Já inicia um novo GUID
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false; 
        public string Name { get; set; }

        // 0 = Despesa, 1 = Receita (Poderíamos usar um Enum aqui no futuro)
        public int TypeCategory { get; set; }

        // Chave Estrangeira para o Usuário
        public Guid UserId { get; set; }

        // Propriedade de Navegação
        public virtual User User { get; set; }
        public virtual ICollection<TransactionVO> Transactions { get; set; } 

        public CategoryVO() 
        {
            Transactions = new List<TransactionVO>();
        }
    }
}
