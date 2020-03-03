using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AvaliacaoGibarco.BackEnd.Api
{
    public class JsonConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //compacta retorno de cada requisição realizada para api.
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            var json = config.Formatters.JsonFormatter;

            json.SupportedMediaTypes.Clear();
            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));

            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;


            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}