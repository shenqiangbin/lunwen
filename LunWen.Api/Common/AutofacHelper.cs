using Autofac;
using Autofac.Integration.WebApi;
using LunWen.Repository;
using LunWen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LunWen.Api.Common
{
    public class AutofacHelper
    {
        public static void Inject()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var serviceAssembly = System.Reflection.Assembly.GetAssembly(typeof(UserService));
            var repositoryAssembly = System.Reflection.Assembly.GetAssembly(typeof(UserRepository));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(new System.Reflection.Assembly[] { assembly, serviceAssembly, repositoryAssembly })
                .Where(t => IsOk(t)).InstancePerLifetimeScope();

            builder.RegisterApiControllers(assembly);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());                       
        }

        public static bool IsOk(Type t)
        {
            return !t.IsAbstract &&
                (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository") || typeof(ApiController).IsAssignableFrom(t));
        }

    }
}