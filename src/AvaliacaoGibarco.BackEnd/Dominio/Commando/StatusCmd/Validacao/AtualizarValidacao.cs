using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd.Validacao
{
    public class AtualizarValidacao : AbstractValidator<AtualizarCmd>
    {
        public AtualizarValidacao()
        {
            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(n => n.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
