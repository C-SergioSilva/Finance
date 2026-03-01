namespace Finance.Domain.BaseEntity
{
    public abstract class BaseGuid // 'abstract' pois ela não vira uma tabela sozinha
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Já inicia um novo GUID
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false; 
    }
}
