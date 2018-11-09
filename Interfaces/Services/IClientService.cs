using Interfaces.Models;
using System.Collections.Generic;

namespace Interfaces.Services
{
    public interface IClientService
    {
        List<Client> GetLongTermClients();

        List<string> GetLongTermWebAPIClients();
    }
}
