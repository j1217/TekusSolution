using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Interfaces;
using Tekus.Application.DTOs.Provider;
using AutoMapper;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProvidersService _providersService;
        private readonly IMapper _mapper;

        public ProvidersController(IProvidersService providersService, IMapper mapper)
        {
            _providersService = providersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista paginada de proveedores con búsqueda opcional por nombre.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var providers = await _providersService.GetAllAsync(search, page, pageSize);
            return Ok(providers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var provider = await _providersService.GetByIdAsync(id);
            if (provider == null)
                return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProviderDto dto)
        {
            var result = await _providersService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProviderDto dto)
        {
            var updated = await _providersService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _providersService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
