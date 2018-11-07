using Interfaces.Models;
using Interfaces.Repository;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ClientService : IClientService
    {
        private int _i = 0;

        private IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<Client> GetLongTermClients()
        {
            ++_i;

            if (_i == 1)
            {
                Console.WriteLine($" Created Brand New Service object");
            }
            else
            {
                Console.WriteLine($" Old Service object is used { _i } times ");
            }

            return _clientRepository.GetAllClients().Where(w => w.RelationPeriod > 5).ToList();
        }        
    }
}
