using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd.Validacao
{
    public class ObterValidacao : AbstractValidator<ObterCmd>
    {
        public ObterValidacao()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
