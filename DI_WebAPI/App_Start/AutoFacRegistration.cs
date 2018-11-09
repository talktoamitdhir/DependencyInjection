using Autofac;
using Autofac.Integration.WebApi;
using Interfaces.Repository;
using Interfaces.Services;
using Repositories;
using Services;
using System;
using System.Reflection;
using System.Web.Http;

namespace DI_WebAPI.App_Start
{
    public static class AutoFacRegistration
    {
        internal static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterExternalDependencies(builder);

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterExternalDependencies(ContainerBuilder builder)
        {
            //Register_PerRequestService_PerRequestRepository(builder);

            Register_PerRequestService_SingleRepository(builder);
        }
        
        private static void Register_PerRequestService_PerRequestRepository(ContainerBuilder builder)
        {
            builder.RegisterType<ClientService>()
                .As<IClientService>();

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>();
        }

        private static void Register_PerRequestService_SingleRepository(ContainerBuilder builder)
        {
            builder.RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerRequest();

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .SingleInstance();
        }

    }
}