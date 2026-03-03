using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly IUserService userService;
        public LoginController(IAuthService service, IUserService userService)
        {
            this.service = service;
            this.userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
           var user = await userService.Authenticate(loginRequest.Email, loginRequest.Password);

            if (user != null)
            {
                var token = service.GeradorJwtToken(user);

                // 3. Retornamos o token e os dados básicos do usuário (útil para o Angular)
                return Ok(new
                {
                    Token = token,
                    User = new { user.IdUser, user.Name, user.Email } 
                });
            }
            return Unauthorized("Usuário inexistente ou com e-mail ou senha inválidos");
        }
    }
}
