using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Zebpay_Jayesh
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                 name: "RateConversionApi",
                 routeTemplate: "api/{controller}/{currency}/{amount}",
                  defaults: new { currency = "USD", amount = 1.0 }
             );
        }
    }
}
