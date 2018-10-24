using Autofac;
using System;
using TechnossusOne.Interfaces;
using TechnossusOne.Services;

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
            //DisplayClients();

            DisplayClientsUsingAutofac();
        }

        private static void BuildAutoFacContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<ClientRepository>()
                .As<IClientRepository>()
              ;
            //.SingleInstance();
            //.InstancePerDependency();


            builder
                .RegisterType<ClientService>()
                .As<IClientService>()
               ;
            //.SingleInstance();
            //.InstancePerDependency();
            //.InstancePerLifetimeScope();

            _container = builder.Build();
        }

        private static void DisplayClients()
        {
            //ClientRepository clientRepository = new ClientRepository();

            //ClientService clientService = new ClientService(clientRepository);

            //clientService.GetLongTermClients().ForEach((f) =>
            //{
            //    Console.WriteLine($" Client Name : {f.Name} \n\n");
            //    Console.ReadKey();
            //});
        }

        private static void DisplayClientsUsingAutofac()
        {
            BuildAutoFacContainer();

            BeginScopeOne();

            BeginScopeTwo();
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
