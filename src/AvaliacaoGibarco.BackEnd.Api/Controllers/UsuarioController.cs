using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api.Controllers
{
    [RoutePrefix("Autenticacao")]
    [Description("Gerenciar País")]
    public class UsuarioController : MainController
    {
        public UsuarioController(INotificador notificador,
                                 IUsuarioServ serv)
            : base(notificador)
        {
            _serv = serv;
        }

        private readonly IUsuarioServ _serv;

        [HttpPost, Route]
        [Display(Name = "Logar")]
        public async Task<HttpResponseMessage> Post([FromBody] LogarCmd parametros)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _serv.Logar(parametros);

            return CustomResponse(parametros);
        }
    }
}