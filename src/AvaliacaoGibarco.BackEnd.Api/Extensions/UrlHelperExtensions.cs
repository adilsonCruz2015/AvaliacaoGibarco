using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Routing;

namespace AvaliacaoGibarco.BackEnd.Api.Extensions
{
    public static class UrlHelperExtensions
    {
        #region Absolute

        public static string Absolute(this UrlHelper urlHelper, string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}",
                url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }

        #endregion

        #region Relativo
        public static string Relativo(this UrlHelper urlHelper, string caminho)
        {
            string referencia = urlHelper.Absolute("~/");
            string resultado = Regex.Replace(caminho.Trim(), referencia, "~/", RegexOptions.IgnoreCase);
            resultado = Regex.Replace(resultado, @"^(\s*/)([^/]+)", "~/");
            return resultado;
        }
        #endregion
    }
}