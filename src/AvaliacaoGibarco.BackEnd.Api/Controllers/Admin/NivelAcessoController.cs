using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api.Controllers.Admin
{
    [RoutePrefix("Admin/NivelAcesso")]
    [Description("Gerenciar País")]
    public class NivelAcessoController : MainController
    {
        public NivelAcessoController(INotificador notificador,
                                     INivelAcessoServ serv)
            :base(notificador)
        {
            _serv = serv;
        }

        private readonly INivelAcessoServ _serv;

        [HttpGet, Route("{codigo}")]
        [Display(Name = "Obter")]
        public async Task<HttpResponseMessage> Get([FromUri] ObterCmd parametros)
        {
            NivelAcesso nivelAcesso = null;

            if (!object.Equals(parametros, null))
                nivelAcesso = _serv.Obter(parametros);

            return CustomResponse(nivelAcesso);
        }

        [HttpPut, Route("{Codigo}")]
        [Display(Name = "Atualizar")]
        public async Task<HttpResponseMessage> Put([FromUri]int codigo, [FromBody]AtualizarCmd parametros)
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

            NivelAcesso[] nivelAcessos = _serv.Filtrar(parametros);

            return CustomResponse(nivelAcessos);
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