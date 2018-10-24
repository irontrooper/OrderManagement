using OrderManagement.Data;
using OrderManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace OrderManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            CustomInit();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //try
            //{
            //    CrsRepository repository = new CrsRepository();
            //    repository.Context.Database.Connection.Open();
            //    repository.Context.Database.Connection.Close();
            //}
            //catch (Exception exp)
            //{

            //}
        }

        /// <summary>
        /// Init Modules
        /// </summary>
        private void CustomInit()
        {
            Data.InitModule.Init();
            Domain.InitModule.Init();
            Services.InitModule.Init();
        }
    }
}
