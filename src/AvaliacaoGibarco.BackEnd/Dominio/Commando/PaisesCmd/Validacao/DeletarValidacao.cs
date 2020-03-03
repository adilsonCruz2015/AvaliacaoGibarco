using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd.Validacao
{
    public class DeletarValidacao : AbstractValidator<DeletarCmd>
    {
        public DeletarValidacao()
        {
            RuleFor(f => f.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
