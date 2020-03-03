using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum
{
    public abstract class BaseService
    {
        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        private readonly INotificador _notificador;

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                Notificar(erro.ErrorMessage);
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        public bool HaNotificacoes()
        {
            return _notificador.TemNotificacao();
        }
    }
}
