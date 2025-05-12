using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tekus.Application.DTOs.Reports;
using Tekus.Application.Interfaces;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Application.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ApplicationDbContext _context;

        public ReportsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SummaryReportDto> GetSummaryReportAsync()
        {
            // Proveedores por país (basado en servicios ofrecidos)
            var providersPerCountry = await _context.Services
                .Include(s => s.Country)
                .Include(s => s.Provider)
                .GroupBy(s => s.Country.Name)
                .Select(g => new CountryProviderSummaryDto
                {
                    Country = g.Key,
                    ProviderCount = g.Select(s => s.ProviderId).Distinct().Count()
                })
                .ToListAsync();

            // Servicios por país
            var servicesPerCountry = await _context.Services
                .Include(s => s.Country)
                .GroupBy(s => s.Country.Name)
                .Select(g => new CountryServiceSummaryDto
                {
                    Country = g.Key,
                    ServiceCount = g.Count()
                })
                .ToListAsync();

            return new SummaryReportDto
            {
                ProvidersPerCountry = providersPerCountry,
                ServicesPerCountry = servicesPerCountry
            };
        }
    }
}
