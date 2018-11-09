using Interfaces.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace DI_WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private IClientService _clientService;

        public ValuesController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET api/values
        [Route("InstancePerRequest-Service_InstancePerRequest-Repository")]
        public List<string> Get()
        {
            return _clientService.GetLongTermWebAPIClients();
        }

    }
}
