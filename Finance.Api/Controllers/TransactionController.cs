using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.Api.Controllers
{
    [Authorize] // Bloqueia acesso sem Token JWT
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionVO vo)
        {
            // Extrai o ID do usuário logado diretamente do Token
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            vo.UserId = userId;

            var result = await _service.Create(vo);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var transactions = await _service.GetByUser(userId);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _service.GetById(id);
            return Ok(transaction);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TransactionVO vo)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            vo.UserId = userId;

            await _service.Update(vo);
            return NoContent(); // 204 Sucesso sem conteúdo
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var balance = await _service.GetBalance(userId);
            return Ok(new { balance });
        }
    }
}