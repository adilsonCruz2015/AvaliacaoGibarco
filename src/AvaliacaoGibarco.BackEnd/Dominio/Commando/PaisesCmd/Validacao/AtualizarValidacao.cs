using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd.Validacao
{
    public class AtualizarValidacao : AbstractValidator<AtualizarCmd>
    {
        public AtualizarValidacao()
        {
            RuleFor(f => f.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
