using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Api.Auxiliar
{
    /// <summary>
    /// Descrição de onde peguei as informações abaixo
    /// https://simpleinjector.readthedocs.io/en/latest/webapiintegration.html
    /// </summary>
    public class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = null;
            if(TryRetrieveToken(request, out token))
            {
                try
                {
                    var serv = ServiceLocator.Current.GetInstance<IAutenticacaoServ>();
                    Autenticacao autenticacao = serv.Inicializar(new InicializarCmd() { Token = token });
                }
                catch(Exception ex)
                {
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}