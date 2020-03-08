using AvaliacaoGibarco.BackEnd.Api.Filters;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api
{
    public class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new EstaAutenticadoAttribute());
        }
    }
}