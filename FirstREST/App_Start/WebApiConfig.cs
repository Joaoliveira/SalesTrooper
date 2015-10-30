using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;

namespace FirstREST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //Attr routing
            config.MapHttpAttributeRoutes();

            //Convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //Se quisermos json descomentar linha abaixo
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
