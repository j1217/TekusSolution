using System;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa el valor monetario de un servicio con validación de monto positivo.
/// </summary>
public sealed class Price : IEquatable<Price>
{
    public decimal Value { get; }

    // Constructor público con validación
    public Price(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.");

        Value = value;
    }

    // Constructor privado para EF Core
    private Price() { }

    public override string ToString() => Value.ToString("C");

    // Implementación de igualdad por valor
    public override bool Equals(object obj) => Equals(obj as Price);

    public bool Equals(Price other) => other != null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
