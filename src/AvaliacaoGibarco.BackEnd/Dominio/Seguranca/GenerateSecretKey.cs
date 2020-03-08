using System;
using System.Security.Cryptography;

namespace AvaliacaoGibarco.BackEnd.Dominio.Seguranca
{
    public static class GenerateSecretKey
    {
        public static string Generate()
        {
            var hmac = new HMACSHA256();

            return  Convert.ToBase64String(hmac.Key);
        }
    }
}
