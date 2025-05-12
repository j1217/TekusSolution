using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tekus.Domain.Entities;
using Tekus.Domain.ExternalContracts;
using Tekus.Domain.ValueObjects;

namespace Tekus.Infrastructure.Persistence
{
    /// <summary>
    /// Clase utilitaria para insertar datos iniciales en la base de datos con fines de prueba.
    /// </summary>
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var countryService = scope.ServiceProvider.GetRequiredService<ICountryApiService>();

            if (context.Providers.Any() || context.Services.Any())
                return; // Ya se insertaron datos

            // Obtener países desde el API
            var countries = await countryService.GetCountriesAsync();
            var selectedCountries = countries.Take(5).ToList(); // Tomamos solo 5 para ejemplo

            // Insertar países
            foreach (var country in selectedCountries)
            {
                if (!context.Countries.Any(c => c.Id == country.Id))
                    context.Countries.Add(country);
            }

            await context.SaveChangesAsync();

            // Crear 10 proveedores
            var providers = new List<Provider>();
            for (int i = 1; i <= 10; i++)
            {
                var email = new Email($"proveedor{i}@tekus.co");
                var provider = new Provider($"NIT-100{i}", $"Proveedor {i}", email);

                // Agregar campos personalizados
                provider.CustomFields.Add(new ProviderCustomField("Campo Personalizado", $"Valor {i}", provider.Id));
                provider.CustomFields.Add(new ProviderCustomField("Mascotas en nómina", (i * 2).ToString(), provider.Id));

                providers.Add(provider);
                context.Providers.Add(provider);
            }

            await context.SaveChangesAsync();

            // Crear 10 servicios
            var rnd = new Random();
            var services = new List<Service>();
            for (int i = 1; i <= 10; i++)
            {
                var provider = providers[i % providers.Count];
                var country = selectedCountries[i % selectedCountries.Count];

                var service = new Service(
                    name: $"Servicio {i}",
                    hourlyRate: rnd.Next(50, 200),
                    providerId: provider.Id,
                    countryId: country.Id
                );

                context.Services.Add(service);
            }

            await context.SaveChangesAsync();
        }
    }
}
