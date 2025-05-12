using FluentValidation;
using Tekus.Application.DTOs.Provider;

namespace Tekus.Application.Validators.Provider
{
    /// <summary>
    /// Validador para los datos de creación de un proveedor.
    /// Verifica que el NIT, nombre y correo electrónico sean válidos.
    /// </summary>
    public class CreateProviderDtoValidator : AbstractValidator<CreateProviderDto>
    {
        public CreateProviderDtoValidator()
        {
            RuleFor(x => x.NIT)
                .NotEmpty()
                .WithMessage("El NIT del proveedor es obligatorio.")
                .MaximumLength(20)
                .WithMessage("El NIT no puede superar los 20 caracteres.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre del proveedor es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                .WithMessage("El correo electrónico no tiene un formato válido.");
        }
    }
}
