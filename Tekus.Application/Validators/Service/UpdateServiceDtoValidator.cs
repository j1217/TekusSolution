using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tekus.Application.DTOs.Services;

namespace Tekus.Application.Validators.Service
{
    /// <summary>
    /// Validador para la actualización de un servicio.
    /// </summary>
    public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del servicio es obligatorio.")
                .Length(3, 100).WithMessage("El nombre del servicio debe tener entre 3 y 100 caracteres.");

            RuleFor(x => x.HourlyRate)
                .GreaterThan(0).WithMessage("El valor por hora debe ser mayor que 0.");

            RuleFor(x => x.CountryIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos un país.")
                .Must(ids => ids.All(id => id > 0)).WithMessage("Los IDs de los países deben ser válidos.");
        }
    }
}

