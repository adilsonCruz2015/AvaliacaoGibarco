using AvaliacaoGibarco.BackEnd.Data.Persistencia.Contexto;
using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using SimpleInjector;

namespace AvaliacaoGibarco.BackEnd.Idc.Modulos
{
    public static class InfraestruturaModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<IConexao, Conexao>(Lifestyle.Scoped);            
        }
    }
}
