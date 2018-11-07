using Repositories.Models;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
    }
}
