using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using DotNet_Framework_WebApp.Models;

namespace DotNet_Framework_WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Set the database initializer
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());

            // Force database initialization
            using (var context = new AppDbContext())
            {
                context.Database.Initialize(force: true);
            }
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception is ViewStateException)
            {
                // Log atau tangani error di sini
                Server.ClearError();
                Response.Redirect("~/ErrorPage.aspx");
            }
        }

    }
}