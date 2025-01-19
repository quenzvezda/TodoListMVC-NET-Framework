using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DotNet_Framework_WebApp.Models;

namespace DotNet_Framework_WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Set database initializer
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TodoItemContext>());

            // Tes koneksi database
            using (var context = new TodoItemContext())
            {
                try
                {
                    var count = context.TodoItems.Count();
                    System.Diagnostics.Debug.WriteLine($"Jumlah TodoItems: {count}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
                }
            }
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}