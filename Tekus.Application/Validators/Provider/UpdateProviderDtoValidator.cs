using FluentValidation;
using Tekus.Application.DTOs.Provider;

namespace Tekus.Application.Validators.Provider
{
    public class UpdateProviderDtoValidator : AbstractValidator<UpdateProviderDto>
    {
        public UpdateProviderDtoValidator()
        {
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
