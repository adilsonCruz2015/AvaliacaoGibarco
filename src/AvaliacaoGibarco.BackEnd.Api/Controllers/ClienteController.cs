using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd;
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
    [RoutePrefix("Cliente")]
    [Description("Gerenciar Cliente")]
    public class ClienteController : MainController
    {
        public ClienteController(INotificador notificador,
                                 IClienteServ serv)
            : base(notificador)
        {
            _serv = serv;
        }

        private readonly IClienteServ _serv;

        [HttpGet, Route("{Codigo}")]
        [Display(Name = "Obter")]
        public async Task<HttpResponseMessage> Get([FromUri] ObterCmd parametros)
        {
            Cliente cliente = null;

            if (!object.Equals(parametros, null))
                cliente = _serv.Obter(parametros);

            return CustomResponse(cliente);
        }

        [HttpPut, Route("{Codigo}")]
        [Display(Name = "Atualizar")]
        public async Task<HttpResponseMessage> Put([FromUri] int codigo, [FromBody] AtualizarCmd parametros)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!object.Equals(parametros, null))
                parametros.Codigo = codigo;

            _serv.Atualizar(parametros);

            return CustomResponse(parametros);
        }

        [HttpDelete, Route("{Codigo}")]
        [Display(Name = "Deletar")]
        public async Task<HttpResponseMessage> Delete([FromUri] DeletarCmd parametros)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!object.Equals(parametros, null))
                _serv.Delete(parametros);

            return CustomResponse();
        }

        [HttpGet, Route]
        [Display(Name = "Filtrar")]
        public async Task<HttpResponseMessage> Get([FromUri] FiltrarCmd parametros)
        {
            if (object.Equals(parametros, null))
                parametros = new FiltrarCmd();

            Cliente[] usuario = _serv.Filtrar(parametros);

            return CustomResponse(usuario);
        }

        [HttpPost, Route]
        [Display(Name = "Inserir")]
        public async Task<HttpResponseMessage> Post([FromBody]InserirCmd parametros)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (object.Equals(parametros, null))
                parametros = new InserirCmd();

            _serv.Inserir(parametros);

            return CustomResponse(parametros);
        }
    }
}