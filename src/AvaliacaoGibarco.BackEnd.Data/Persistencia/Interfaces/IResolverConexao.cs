using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces
{
    public interface IResolverConexao
    {
        string ObterReferencia(string nome);

        string ObterConexao(Banco banco);
    }
}
