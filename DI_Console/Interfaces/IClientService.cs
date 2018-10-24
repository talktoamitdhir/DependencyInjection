using System.Collections.Generic;
using TechnossusOne.Models;

namespace TechnossusOne.Interfaces
{
    public interface IClientService
    {
        List<Client> GetLongTermClients();

    }
}
