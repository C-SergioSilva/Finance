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
    } 
}
