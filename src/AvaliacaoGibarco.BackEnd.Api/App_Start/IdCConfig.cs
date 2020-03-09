using AvaliacaoGibarco.BackEnd.Api.App_Start.SimpleInjectorCustom;
using AvaliacaoGibarco.BackEnd.Api.Auxiliar;
using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Notificacoes;
using CommonServiceLocator;
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
            container.Register<ITokenManager, TokenManager>(Lifestyle.Scoped);

            Idc.IdC.Carregar(container);
            container.RegisterWebApiControllers(config);

            container.Verify();

            // Adapter for Service Locator
            var adapter = new SimpleInjectorServiceLocatorAdapter(container);
            ServiceLocator.SetLocatorProvider(() => adapter);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}