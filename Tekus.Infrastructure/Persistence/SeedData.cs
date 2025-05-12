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
    /// Incluye países desde API externa, proveedores con campos personalizados y servicios.
    /// </summary>
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var countryService = scope.ServiceProvider.GetRequiredService<ICountryApiService>();

            // Evitar reinserción si ya existen datos
            if (context.Providers.Any() || context.Services.Any())
                return;

            // Obtener países desde la API REST externa
            var countries = await countryService.GetCountriesAsync();
            var selectedCountries = countries.Take(5).ToList(); // Seleccionamos 5 países de ejemplo

            // Insertar países si no existen
            foreach (var country in selectedCountries)
            {
                if (!context.Countries.Any(c => c.Id == country.Id))
                    context.Countries.Add(country);
            }

            await context.SaveChangesAsync();

            // Crear 10 proveedores con campos personalizados
            var providers = new List<Provider>();
            for (int i = 1; i <= 10; i++)
            {
                var email = new Email($"proveedor{i}@tekus.co");
                var provider = new Provider($"NIT-100{i}", $"Proveedor {i}", email);

                provider.CustomFields.Add(new ProviderCustomField("Campo Personalizado", $"Valor {i}", provider.Id));
                provider.CustomFields.Add(new ProviderCustomField("Mascotas en nómina", (i * 2).ToString(), provider.Id));

                providers.Add(provider);
                context.Providers.Add(provider);
            }

            await context.SaveChangesAsync();

            // Crear 10 servicios con duración basada en fechas
            var rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                var provider = providers[i % providers.Count];
                var country = selectedCountries[i % selectedCountries.Count];

                // Precio aleatorio entre 50 y 150 USD
                var price = new Price((decimal)(rnd.NextDouble() * 100 + 50));

                // Fechas aleatorias de inicio y fin (duración entre 1 y 5 días)
                var startDate = DateTime.UtcNow.AddDays(-rnd.Next(0, 10)); // Desde hoy hacia atrás unos días
                var endDate = startDate.AddDays(rnd.Next(1, 6)); // Duración entre 1 y 5 días

                var duration = new ServiceDuration(startDate, endDate);

                var service = new Service(
                    name: $"Servicio {i}",
                    providerId: provider.Id,
                    countryId: country.Id,
                    price: price,
                    duration: duration
                );

                context.Services.Add(service);
            }

            await context.SaveChangesAsync();
        }
    }
}
