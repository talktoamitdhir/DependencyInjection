using Autofac;
using Interfaces.Repository;
using Interfaces.Services;
using Repositories;
using Services;
using System;

namespace TechnossusOne
{
    class Program
    {
        private static IContainer _container
        {
            get; set;
        }

        static void Main(string[] args)
        {
            //DisplayClientsWithoutAutoFac();

            DisplayClientsUsingAutofac();
        }

        private static void DisplayClientsWithoutAutoFac()
        {
            ClientRepository clientRepository = new ClientRepository();

            ClientService clientService = new ClientService(clientRepository);

            clientService.GetLongTermClients().ForEach((f) =>
            {
                Console.WriteLine($" Client Name : {f.Name} \n\n");
                Console.ReadKey();
            });
        }

        private static void DisplayClientsUsingAutofac()
        {
            BuildAutoFacContainer();

            BeginScopeOne();

            BeginScopeTwo();
        }

        private static void BuildAutoFacContainer()
        {
            var builder = new ContainerBuilder();

            Register_PerDependencyService_PerDependencyRepository(builder);

            //Register_PerDependencyService_SingleRepository(builder);

            //Register_PerDependencyService_LifeTimeRepository(builder);

            //Register_SingleService_LifeTimeRepository(builder); Explain me

            //Register_LifeTimeService_LifeTimeRepository(builder);

            _container = builder.Build();
        }
        
        private static void Register_PerDependencyService_PerDependencyRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerDependency();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerDependency();
        }

        private static void Register_PerDependencyService_SingleRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerDependency();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .SingleInstance();
        }

        private static void Register_PerDependencyService_LifeTimeRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerDependency();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerLifetimeScope();
        }

        private static void Register_SingleService_LifeTimeRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .SingleInstance();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerLifetimeScope();
        }

        private static void Register_LifeTimeService_LifeTimeRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerLifetimeScope();
        }

        private static void Register_SingleService_PerDependencyRepository(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
                .SingleInstance();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerDependency();
        }

        private static void BeginScopeOne()
        {
            Console.WriteLine("\n First lifetime scope \n");

            using (var scope = _container.BeginLifetimeScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    var service = scope.Resolve<IClientService>();

                    service.GetLongTermClients().ForEach((f) =>
                    {
                        Console.WriteLine($" Client Name : {f.Name} \n\n ");
                        Console.ReadKey();
                    });

                }
            }
        }

        private static void BeginScopeTwo()
        {
            Console.WriteLine("\n Second lifetime scope \n");

            using (var scope = _container.BeginLifetimeScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    var service = scope.Resolve<IClientService>();

                    service.GetLongTermClients().ForEach((f) =>
                    {
                        Console.WriteLine($" Client Name : {f.Name} \n\n");
                        Console.ReadKey();
                    });

                }
            }
        }
    }
}
