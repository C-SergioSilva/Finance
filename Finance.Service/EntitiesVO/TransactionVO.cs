namespace Finance.Service.EntitiesVO
{
    public class TransactionVO
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Já inicia um novo GUID
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Relacionamentos (FKs)
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
