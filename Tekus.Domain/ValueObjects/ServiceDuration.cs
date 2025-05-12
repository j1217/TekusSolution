using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa un rango de fechas para la duración de un servicio.
/// </summary>
public sealed class ServiceDuration
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public ServiceDuration(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

        StartDate = startDate;
        EndDate = endDate;
    }

    public TimeSpan Duration => EndDate - StartDate;
}
