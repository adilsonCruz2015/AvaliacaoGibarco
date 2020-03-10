using AvaliacaoGibarco.BackEnd.Api.Extensions;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Ambiente;
using System.Web.Http.Routing;

namespace AvaliacaoGibarco.BackEnd.Api.Auxiliar
{
    public class ResolverLocalArquivo : IResolverLocalArquivo
    {
        private readonly UrlHelper _urlHelper = new UrlHelper();

        public string ParaAbsoluto(string caminho)
        {
            return _urlHelper.Absolute(caminho);
        }

        public string ParaRelativo(string caminho)
        {
            return _urlHelper.Relativo(caminho);
        }
    }
}