using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Seguranca;
using CommonServiceLocator;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AvaliacaoGibarco.BackEnd.Api.Filters
{
    public class EstaAutenticadoAttribute : AuthorizeAttribute
    {
        bool ChecarAutorizacao(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            string autorizacao = null;

            if (!request.Headers.TryGetValues("Authorization", out IEnumerable<string> authzHeaders) || authzHeaders.Count() > 1)
                return false;

            string bearerToken = authzHeaders.FirstOrDefault()?.Trim() ?? string.Empty;
            autorizacao = Regex.Replace(bearerToken, @"^Bearer ", "", RegexOptions.IgnoreCase)?.Trim();

            if (string.IsNullOrWhiteSpace(autorizacao))
                return false;

            var principal = AuthJwtToken(autorizacao);

            if (Equals(principal, null))
                return false;           


            return !Equals(principal, null);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return ChecarAutorizacao(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);

            actionContext.Response = actionContext.ControllerContext.Request.
                CreateResponse(HttpStatusCode.Unauthorized,
                new { Mensagem = "Acesso não autorizado" });
        }

        private bool ValidateToken(string token, out string username)
        {
            username = null;
            var tokenManager = ServiceLocator.Current.GetInstance<ITokenManager>();

            var simplePrinciple = tokenManager.GetPrincipal(token);
            if (Equals(simplePrinciple, null))
                return false;

            var identity = simplePrinciple.Identity as ClaimsIdentity;
            if (Equals(identity, null))
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrWhiteSpace(username))
                return false;

            Autenticacao autenticacao = Iniciar(token);

            if(Equals(autenticacao, null))
            {
                return false;
            }
            else 
            {
                IUsuarioServ serv = ServiceLocator.Current.GetInstance<IUsuarioServ>();
                var userBD = serv.ObterUserName(autenticacao.Usuario.Email);

                if (Equals(userBD, null))
                    return false;
            }

            

            return true;
        }

        protected IPrincipal AuthJwtToken(string token)
        {
            string username;

            if(ValidateToken(token, out username))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return user;
            }

            return null;
        }

        protected Autenticacao Iniciar(string token)
        {
            var serv = ServiceLocator.Current.GetInstance<IAutenticacaoServ>();
            Autenticacao autenticacao = serv.Inicializar(new InicializarCmd() { Token = token });

            return autenticacao;
        }
    }
}