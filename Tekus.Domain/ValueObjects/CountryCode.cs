using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa un código de país ISO de dos letras.
/// </summary>
public sealed class CountryCode
{
    public string Value { get; }

    public CountryCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código del país es requerido.");

        if (!Regex.IsMatch(value, @"^[A-Z]{2}$"))
            throw new ArgumentException("El código del país debe tener dos letras mayúsculas (ISO 3166-1 alpha-2).");

        Value = value;
    }

    public override string ToString() => Value;
}

