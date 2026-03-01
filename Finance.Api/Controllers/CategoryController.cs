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
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("Usuário não identificado no token.");

                categoryVO.UserId = Guid.Parse(userIdClaim);

                var result = await _service.Create(categoryVO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logar o erro 'ex' aqui seria o ideal
                return BadRequest(new { message = "Erro ao criar categoria.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("Usuário não identificado no token.");

                var userId = Guid.Parse(userIdClaim);
                var categories = await _service.GetByUser(userId);

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar categorias.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetById(id);
                if (result == null) return NotFound("Categoria não encontrada.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar categoria por ID.", details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryVO categoryVO)
        {
            try
            {
                await _service.Update(categoryVO);
                return Ok(new { message = "Categoria atualizada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao atualizar categoria.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return Ok(new { message = "Categoria removida com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao remover categoria.", details = ex.Message });
            }
        }
    }
}