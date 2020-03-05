using System.Web.Http;
using Swashbuckle.Application;
using System.Reflection;
using System;
using System.Linq;

namespace AvaliacaoGibarco.BackEnd.Api
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            string versao = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion($"v1", $"Documentação API Gestão de Clientes {versao}");

                    c.IncludeXmlComments(string.Format(
                    @"{0}\bin\AvaliacaoGibarco.BackEnd.Api.xml",
                    AppDomain.CurrentDomain.BaseDirectory));

                    c.DescribeAllEnumsAsStrings();
                    c.ResolveConflictingActions(x => x.First());
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle($"Documentação API Gestão de Clientes {versao}");

                    c.SupportedSubmitMethods(new string[] { });
                });
        }
    }
}
