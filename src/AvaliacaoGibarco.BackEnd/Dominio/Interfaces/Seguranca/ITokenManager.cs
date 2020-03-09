using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using System.Security.Claims;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca
{
    public interface ITokenManager
    {
        string GenerateToken(Autenticacao dados);

        int Decode(string token);

        ClaimsPrincipal GetPrincipal(string token);
    }
}
