using Finance.Domain.Models;
using Finance.Service.EntitiesVO;

namespace Finance.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserVO> Create(UserVO user);
        Task Update(UserVO UserVO);
        Task<IEnumerable<UserVO>> GetAll();
        Task<UserVO> Authenticate(string email, string password);
        Task<UserVO> GetById(Guid id);
        Task Deleted(Guid Id); 



    }
}
