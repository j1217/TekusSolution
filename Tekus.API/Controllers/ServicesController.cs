using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Interfaces;
using Tekus.Application.DTOs.Services;
using AutoMapper;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        private readonly IMapper _mapper;

        public ServicesController(IServicesService servicesService, IMapper mapper)
        {
            _servicesService = servicesService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista paginada de servicios, con búsqueda opcional.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var services = await _servicesService.GetAllAsync(search, page, pageSize);
            return Ok(services);
        }

        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _servicesService.GetByIdAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        /// <summary>
        /// Crea un nuevo servicio.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto dto)
        {
            var result = await _servicesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Actualiza un servicio existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceDto dto)
        {
            var updated = await _servicesService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>
        /// Elimina un servicio por su ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _servicesService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
