using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd.Validacao
{
    public class InicializarValidacao : AbstractValidator<InicializarCmd>
    {
        public InicializarValidacao()
        {
            RuleFor(t => t.Token)
                .NotEmpty().
                WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(50, 1000)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


        }
    }
}
