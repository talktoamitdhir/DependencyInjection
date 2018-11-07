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
            DisplayClientsWithoutAutoFac();

            //DisplayClientsUsingAutofac();
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

            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
            .InstancePerDependency();
            //.SingleInstance();
            //.InstancePerLifetimeScope();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
            .InstancePerDependency();
            //.SingleInstance();
            //.InstancePerLifetimeScope();

            _container = builder.Build();
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
