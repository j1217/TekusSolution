using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa el valor monetario de un servicio con validación de monto positivo.
/// </summary>
public sealed class Price
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.");

        Value = value;
    }

    public override string ToString() => Value.ToString("C");
}

