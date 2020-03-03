using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd.Validacao
{
    public class InserirValidacao : AbstractValidator<InserirCmd>
    {
        public InserirValidacao()
        {
            RuleFor(f => f.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
