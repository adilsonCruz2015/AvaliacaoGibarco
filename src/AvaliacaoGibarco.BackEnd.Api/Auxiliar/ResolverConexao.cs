using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.Collections.Generic;
using System.Configuration;

namespace AvaliacaoGibarco.BackEnd.Api.Auxiliar
{
    public class ResolverConexao : IResolverConexao
    {
        private IDictionary<Banco, string> _resultado = new Dictionary<Banco, string>()
        {
            {Banco.AvaliacaoGibarco, "AvaliacaoGibarco" }
        };

        public string ObterConexao(Banco banco)
        {
            return ObterReferencia(ObterNomeDaConnectionString(banco));
        }

        public string ObterReferencia(string nome)
        {
            return ConfigurationManager.ConnectionStrings[nome].ToString();
        }

        private string ObterNomeDaConnectionString(Banco banco)
        {
            return _resultado[banco];
        }
    }
}