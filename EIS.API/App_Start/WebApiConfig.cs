using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EIS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.MessageHandlers.Add(new EISAPIKeyHandler());
            config.Filters.Add(new EISExceptionAttribute());
        }
    }
}
