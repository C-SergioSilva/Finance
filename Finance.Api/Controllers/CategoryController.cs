using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryVO categoryVO)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Usuário não identificado no token.");

            categoryVO.UserId = Guid.Parse(userIdClaim);

            var result = await _service.Create(categoryVO);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Usuário não identificado no token.");

            var userId = Guid.Parse(userIdClaim);
            var categories = await _service.GetByUser(userId);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var result = await _service.GetById(id);
            if (result == null) return NotFound("Categoria não encontrada.");

            return Ok(result);


        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryVO categoryVO)
        {

            await _service.Update(categoryVO);
            return Ok(new { message = "Categoria atualizada com sucesso!" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _service.Delete(id);
            return Ok(new { message = "Categoria removida com sucesso!" });

        }
    }
}