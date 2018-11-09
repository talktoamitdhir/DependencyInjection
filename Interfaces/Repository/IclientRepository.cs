using Interfaces.Models;
using System.Collections.Generic;

namespace Interfaces.Repository
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();

        string GetAllWebAPIClients();
    }
}
