using FluentValidation;
using Siagri.Clima.Business.Dtos;

namespace Siagri.Clima.Business.Validators
{
    public class CidadeDtoValidator : AbstractValidator<CidadeDto>
    {
        public CidadeDtoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O campo Nome não pode ser vazio.");
        }
    }
}
