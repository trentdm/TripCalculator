using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace TripCalculator
{
    public class SerializerConfig
    {
        public static void RegisterSerializers()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}