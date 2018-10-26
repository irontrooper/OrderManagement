using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace OrderManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            OrderManagementCore.Instance.RegisterTypes();
            ResolveApiControllers();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        /// <summary>
        /// Resolves the api controllers
        /// </summary>
        private static void ResolveApiControllers()
        {
            var types = OrderManagementCore.Instance.AssemblyTypes.Where(t => t.IsClass && !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                OrderManagementCore.Instance.Container.RegisterType(t, new TransientLifetimeManager());
            });
        }
    }
}

