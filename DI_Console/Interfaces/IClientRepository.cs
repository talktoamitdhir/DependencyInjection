using System.Collections.Generic;
using TechnossusOne.Models;

namespace TechnossusOne.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
    }
}
