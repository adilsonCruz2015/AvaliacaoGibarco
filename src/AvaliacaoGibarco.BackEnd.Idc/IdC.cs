using AvaliacaoGibarco.BackEnd.Idc.Modulos;
using SimpleInjector;

namespace AvaliacaoGibarco.BackEnd.Idc
{
    public static class IdC
    {
        public static void Carregar(Container recipiente)
        {
            ServicoModulo.Carregar(recipiente);
            RepositorioModulo.Carregar(recipiente);
            InfraestruturaModulo.Carregar(recipiente);
        }
    }
}
