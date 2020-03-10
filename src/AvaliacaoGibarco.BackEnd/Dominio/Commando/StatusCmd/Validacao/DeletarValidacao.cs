using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd.Validacao
{
    public class DeletarValidacao : AbstractValidator<DeletarCmd>
    {
        public DeletarValidacao()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
