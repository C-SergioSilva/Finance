using Finance.Service.EntitiesVO;

namespace Finance.Service.Interfaces
{
    public interface IAuthService
    {
        string GeradorJwtToken(UserVO user);
    }
}
