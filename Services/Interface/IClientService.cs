using Repositories.Models;
using System.Collections.Generic;

namespace Services.Interface
{
    public interface IClientService
    {
        List<Client> GetLongTermClients();
    }
}
