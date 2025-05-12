using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tekus.Application.Interfaces;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class SummaryController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        public SummaryController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        /// <summary>
        /// Devuelve un resumen de la cantidad de proveedores y servicios por país.
        /// </summary>
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _reportsService.GetSummaryReportAsync();
            return Ok(result);
        }
    }
}
