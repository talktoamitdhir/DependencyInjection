using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class ClientRepository : IClientRepository
    {
        private int _i = 0;

        public ClientRepository()
        {
        }

        public List<Client> GetAllClients()
        {
            ++_i;

            if (_i == 1)
            {
                Console.WriteLine($" Created Brand New Repository object");
            }
            else
            {
                Console.WriteLine($" Old Repository object is used { _i } times ");
            }

            return new List<Client>() {
                new Client()
                {
                    Id = 1,
                    Name = "Agendia Inc",
                    RelationPeriod = 10
                },
                new Client()
                {
                    Id = 2,
                    Name = "Bio-Rad",
                    RelationPeriod = 1
                }
            };
        }

    }
}
