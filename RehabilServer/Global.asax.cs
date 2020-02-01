using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using RehabilServer.Controllers;
using RehabilServer.Models;
using RehabilServer.Models.Repositories;
using RehabilServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RehabilServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            // Register Controllers
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register ApiController
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register services.
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // Register Repository - Open Generic
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            //builder.RegisterAssemblyTypes(typeof(WebApiApplication).Assembly)
            //.Where(t => t.IsClosedTypeOf(typeof(IRepository<>)) || t.IsClosedTypeOf(typeof(IRepository<>)))
            //.AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(Repository<>)).AsSelf();
            builder.RegisterType(typeof(AccountController)).UsingConstructor(typeof(IAccountService));
            builder.RegisterType(typeof(AccountService)).UsingConstructor(new[] { typeof(IRepository<Account>), typeof(IUnitOfWork) });


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
