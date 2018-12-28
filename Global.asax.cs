using CountingKsDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CountingKs.MessageHandllers;

namespace CountingKs
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            if (!Database.Exists("CountingKsConnectionString"))
            {
                CreateDatabaseIfNotExists<CountingKsContext> db = new CreateDatabaseIfNotExists<CountingKsContext>();
                Database.SetInitializer(new CountingKsDbContextSeeder(new CountingKsContext()));
            }

            //GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyMessageHandler());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
