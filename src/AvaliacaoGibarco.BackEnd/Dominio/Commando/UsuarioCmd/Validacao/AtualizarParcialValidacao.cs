using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao
{
    public class AtualizarParcialValidacao : AbstractValidator<AtualizarParcialCmd>
    {
        public AtualizarParcialValidacao()
        {
            RuleFor(x => x.Nome).Custom((nome, context) => { 
                if(!string.IsNullOrEmpty(nome))
                {
                    if (nome.Length < 3 || nome.Length > 255)
                        context.AddFailure("O campo Nome precisa ter entre 3 e 255 caracteres.");
                }
            });

            RuleFor(x => x.Email).Custom((email, context) => {
                if (!string.IsNullOrEmpty(email))
                {
                    if (email.Length < 3 || email.Length > 255)
                        context.AddFailure("O campo Nome precisa ter entre 3 e 255 caracteres.");
                }
            });
        }
    }
}
