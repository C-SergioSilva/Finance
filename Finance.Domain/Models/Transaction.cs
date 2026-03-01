
using Finance.Domain.BaseEntity;

namespace Finance.Domain.Models
{
    public class Transaction : BaseGuid
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Relacionamentos (FKs)
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }

        // Propriedades de Navegação (Para o Entity Framework entender o triângulo)
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }
}
