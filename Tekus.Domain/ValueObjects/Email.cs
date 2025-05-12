using System.Text.RegularExpressions;

namespace Tekus.Domain.ValueObjects;

/// <summary>
/// Representa un email con validación integrada de formato.
/// </summary>
public sealed class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El correo electrónico es requerido.");

        if (!IsValid(value))
            throw new ArgumentException("El correo electrónico no tiene un formato válido.");

        Value = value;
    }

    private bool IsValid(string email)
    {
        // Validación básica de email con expresión regular
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    public override string ToString() => Value;
}
