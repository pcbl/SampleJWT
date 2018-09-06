using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SampleJWT.Services.Customer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            WebApi.RouteConfigurator.Configure(config);
        }
    }
}
