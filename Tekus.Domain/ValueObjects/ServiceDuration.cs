using System;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa un rango de fechas para la duración de un servicio.
/// </summary>
public sealed class ServiceDuration : IEquatable<ServiceDuration>
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    // Constructor público con validación
    public ServiceDuration(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

        StartDate = startDate;
        EndDate = endDate;
    }

    // Constructor privado requerido por EF Core
    private ServiceDuration() { }

    public TimeSpan Duration => EndDate - StartDate;

    // Implementación de igualdad por valor
    public override bool Equals(object obj) => Equals(obj as ServiceDuration);

    public bool Equals(ServiceDuration other) =>
        other != null && StartDate == other.StartDate && EndDate == other.EndDate;

    public override int GetHashCode() => HashCode.Combine(StartDate, EndDate);
}
