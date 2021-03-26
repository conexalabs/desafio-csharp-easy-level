using FluentValidation;
using Siagri.Clima.Business.Dtos;

namespace Siagri.Clima.Business.Validators
{
    public class CoordenadaDtoValidator : AbstractValidator<CoordenadaDto>
    {
        public CoordenadaDtoValidator()
        {
            RuleFor(c => c.Latitude)
                .NotEmpty()
                .WithMessage("O campo Latitude não pode ser vazio.");

            RuleFor(c => c.Longitude)
                .NotEmpty()
                .WithMessage("O campo Longitude não pode ser vazio.");
        }
    }
}
