using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FirstMVCFiveProject.Data;
using FirstMVCFiveProject.Infrastructure;
using FirstMVCFiveProject.Models;
using FirstMVCFiveProject.Repositories;
using FirstMVCFiveProject.UnitOfWork; // Ensure this is referencing the class, not just the namespace

namespace FirstMVCFiveProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Ensure EF DB initializer (creates DB if it doesn't exist).
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

            // Simple dependency registration
            var container = new SimpleContainer();

            // Register concrete context per resolve
            container.Register<ApplicationDbContext>(() => new ApplicationDbContext());
            // FIX: Use the fully qualified class name if there is a naming conflict
            container.Register<IUnitOfWork>(() => new FirstMVCFiveProject.UnitOfWork.UnitOfWork(new ApplicationDbContext()));
            container.Register<IRepository<Student>>(() => new Repository<Student>(new ApplicationDbContext()));

            // Set MVC dependency resolver
            DependencyResolver.SetResolver(container);
        }
    }
}
