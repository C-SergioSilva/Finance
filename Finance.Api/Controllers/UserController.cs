using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // Endpoint para Registro (Sem [Authorize] pois é público)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserVO vo)
        {
            var result = await _service.Create(vo);
            return Ok(result);
        }

        // Endpoint para buscar os dados do utilizador logado
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _service.GetById(userId);
            return Ok(user);
        }
    }
}