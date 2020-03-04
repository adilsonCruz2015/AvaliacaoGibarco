using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao
{
    public class LogarValidacao : AbstractValidator<LogarCmd>
    {
        public LogarValidacao()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(10, 255)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(e => e.Email)
                .EmailAddress()
                .WithMessage("O campo {PropertyName} não é válido.");

            RuleFor(s => s.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 20)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
