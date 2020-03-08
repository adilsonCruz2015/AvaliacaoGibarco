using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api.Controllers
{
    [RoutePrefix("autenticacao")]
    [Description("Gerenciar Autenticacao")]
    public class AutenticacaoController : MainController
    {
        public AutenticacaoController(INotificador notificador,
                                      IAutenticacaoServ serv)
            :base(notificador)
        {
            _serv = serv;
        }

        private readonly IAutenticacaoServ _serv;

        [HttpGet, Route("{codigo}")]
        [Display(Name = "Obter")]
        public async Task<HttpResponseMessage> Get([FromUri] ObterCmd parametros)
        {
            Autenticacao autenticacao = null;

            if (!object.Equals(parametros, null))
                autenticacao = _serv.Obter(parametros);

            return CustomResponse(autenticacao);
        }

        [AllowAnonymous]
        [HttpPost, Route]
        [Display(Name = "Logar")]
        public async Task<HttpResponseMessage> Post([FromBody]LogarCmd parametros)
        {
            Autenticacao resultado = null;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (object.Equals(parametros, null))
                parametros = new LogarCmd();

            resultado = _serv.Logar(parametros);

            return CustomResponse(resultado);
        }
    }
}