using System.Threading.Tasks;
using Tekus.Application.DTOs.Reports;

namespace Tekus.Application.Interfaces
{
    /// <summary>
    /// Define los servicios de reporte y agregación de datos.
    /// </summary>
    public interface IReportsService
    {
        /// <summary>
        /// Obtiene un resumen general de proveedores y servicios agrupados por país.
        /// </summary>
        Task<SummaryReportDto> GetSummaryReportAsync();
    }
}
