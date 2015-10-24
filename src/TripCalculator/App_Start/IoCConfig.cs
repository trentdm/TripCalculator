using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TripCalculator.Controllers;
using TripCalculator.Services;

namespace TripCalculator
{
    public class IoCConfig
    {
        /// <summary>
        /// For more info see: 
        /// http://autofac.readthedocs.org/en/latest/integration/mvc.html
        /// </summary>
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.Register(c => new Logger())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(TripExpensesController).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(TripExpensesController).Assembly);
            builder.RegisterApiControllers(typeof(TripExpensesController).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}