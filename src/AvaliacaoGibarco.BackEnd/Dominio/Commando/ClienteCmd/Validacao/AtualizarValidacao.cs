using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd.Validacao
{
    public class AtualizarValidacao : AbstractValidator<AtualizarCmd>
    {
        public AtualizarValidacao()
        {
            RuleFor(f => f.Cnpj)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(18)
                .WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

            RuleFor(f => f.RazaoSocial)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(5, 255)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Pais)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
