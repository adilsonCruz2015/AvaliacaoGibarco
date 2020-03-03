using AvaliacaoGibarco.BackEnd.Api.Auxiliar;
using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Notificacoes;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api
{
    public class IdCConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);
            container.Register<IResolverConexao, ResolverConexao>(Lifestyle.Scoped);

            Idc.IdC.Carregar(container);
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}