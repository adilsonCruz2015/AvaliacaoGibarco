using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos;
using SimpleInjector;

namespace AvaliacaoGibarco.BackEnd.Idc.Modulos
{
    public static class ServicoModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<IPaisServ, PaisServ>();
            recipiente.Register<IClienteServ, ClienteServ>();
            recipiente.Register<IUsuarioServ, UsuarioServ>(Lifestyle.Scoped);
            recipiente.Register<IAutenticacaoServ, AutenticacaoServ>(Lifestyle.Scoped);
            recipiente.Register<IStatusServ, StatusServ>(Lifestyle.Scoped);
            recipiente.Register<INivelAcessoServ, NivelAcessoServ>();
        }
    }
}
