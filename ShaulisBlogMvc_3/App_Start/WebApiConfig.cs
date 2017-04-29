using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShaulisBlogMvc_3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Location}",
                defaults: new { Location = RouteParameter.Optional }
            );

            config.EnsureInitialized();
        }
    }
}
