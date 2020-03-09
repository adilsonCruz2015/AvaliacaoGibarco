using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace AvaliacaoGibarco.BackEnd.Api.Auxiliar
{
    public class TokenManager : ITokenManager
    {
        private static readonly string Secret = "TfxZur1C3jASaz+8wACw4R31G1pFCgRv/QWyUfZXkpCem5uLR6MQDZcKz1+9rVqMGlJWkDJA2/js0fHnXhWxeA==";

        public int Decode(string token)
        {
            string result = null;

            SecurityToken securityToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret));
            
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };

            Thread.CurrentPrincipal = handler.ValidateToken(token, parameters, out securityToken);
            HttpContext.Current.User = handler.ValidateToken(token, parameters, out securityToken);

            if(HttpContext.Current.User?.Identity?.IsAuthenticated == true)
            {
                result = HttpContext.Current.User?.Identity?.Name;
                if(string.IsNullOrEmpty(result))
                {
                    Thread.CurrentPrincipal = null;
                    HttpContext.Current.User = null;
                    result = null;
                }
            }

            return Equals(result, null) ? 0 : int.Parse(result);
        }

        public string GenerateToken(Autenticacao dados)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, dados.Codigo.ToString()),
                new Claim(ClaimTypes.PrimarySid, dados.Usuario.Codigo.ToString()),
                new Claim(ClaimTypes.Email, dados.Usuario.Email ?? string.Empty)
            });

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(dados.ExpiraEm),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret));

                if (jwtToken == null) return null;
                
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = securityKey
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}