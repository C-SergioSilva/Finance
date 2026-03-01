using Finance.Domain.BaseEntity;

namespace Finance.Domain.Models
{
    public class Category : BaseGuid
    {
        public string Name { get; set; }

        // 0 = Despesa, 1 = Receita (Poderíamos usar um Enum aqui no futuro)
        public int TypeCategory { get; set; }

        // Chave Estrangeira para o Usuário
        public Guid UserId { get; set; }

        // Propriedade de Navegação
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Category()
        {
            Transactions = new List<Transaction>();
        }
    }
}
