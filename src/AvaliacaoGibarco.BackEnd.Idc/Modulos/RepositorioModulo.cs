using AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using SimpleInjector;

namespace AvaliacaoGibarco.BackEnd.Idc.Modulos
{
    public static class RepositorioModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<IPaisesRep, PaisesRep>();
            recipiente.Register<IClienteRep, ClienteRep>();
        }
    }
}
