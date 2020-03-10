using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao
{
    public class AtualizarValidacao : AbstractValidator<AtualizarCmd>
    {
        public AtualizarValidacao()
        {
            RuleFor(f => f.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(10, 255)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(e => e.Email)
                .EmailAddress()
                .WithMessage("O campo {PropertyName} não é válido.");

            RuleFor(n => n.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 255)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
