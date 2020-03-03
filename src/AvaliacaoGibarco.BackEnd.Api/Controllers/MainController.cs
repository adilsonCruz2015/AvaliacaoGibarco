using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Notificacoes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AvaliacaoGibarco.BackEnd.Api.Controllers
{
    public class MainController : ApiController
    {
        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        private readonly INotificador _notificador;

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected HttpResponseMessage CustomResponse(object result = null)
        {
            HttpResponseMessage resultado = new HttpResponseMessage();

            if (OperacaoValida())
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    success = true,
                    data = result
                });

                return resultado;
            }

            resultado = Request.CreateResponse(HttpStatusCode.BadRequest, new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });

            return resultado;
        }

        protected HttpResponseMessage CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}