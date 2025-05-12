namespace Tekus.Application.DTOs.Reports
{
    /// <summary>
    /// Representa un resumen de indicadores agrupados por país.
    /// </summary>
    public class SummaryReportDto
    {
        /// <summary>
        /// Lista de países con la cantidad total de proveedores asociados a cada uno.
        /// </summary>
        public List<CountryProviderSummaryDto> ProvidersPerCountry { get; set; }

        /// <summary>
        /// Lista de países con la cantidad total de servicios ofrecidos en cada uno.
        /// </summary>
        public List<CountryServiceSummaryDto> ServicesPerCountry { get; set; }
    }

    public class CountryProviderSummaryDto
    {
        public string Country { get; set; }
        public int ProviderCount { get; set; }
    }

    public class CountryServiceSummaryDto
    {
        public string Country { get; set; }
        public int ServiceCount { get; set; }
    }
}
