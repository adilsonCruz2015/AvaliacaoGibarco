using Owin;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();

			WebApiConfig.Register(config);
			JsonConfig.Register(config);
			FilterConfig.Register(config);
			IdCConfig.Register(config);

			SwaggerConfig.Register(config);

			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);			
		}
	}
}