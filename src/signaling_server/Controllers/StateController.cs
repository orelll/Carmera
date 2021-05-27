using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using signaling_server.Socketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signaling_server.Controllers
{
    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private ISocketRepository _socketRepository;

        public StateController(ISocketRepository socketRepository)
        {
            _socketRepository = socketRepository;
        }

        [HttpGet]
        [Route("/home")]
        public virtual IActionResult Index()
        {
            return Ok("Hello world!");
        }

        [HttpGet]
        [Route("/registrations")]
        public virtual IActionResult GetRegistrationsCount()
        {
            return Ok(_socketRepository.GetAllRegistrations().Count());
        }

        [HttpDelete]
        [Route("/registrations")]
        public virtual IActionResult DeleteRegistrations()
        {
            _socketRepository.ClearAllRegistrations();
            return Ok(_socketRepository.GetAllRegistrations().Count());
        }

        [HttpGet]
        [Route("/servers")]
        public virtual IActionResult GetServersCount()
        {
            return Ok(_socketRepository.GetAllServers().Count());
        }

        [HttpDelete]
        [Route("/servers")]
        public virtual IActionResult DeleteRegisteredServers()
        {
            _socketRepository.ClearAllServers();
            return Ok(_socketRepository.GetAllServers().Count());
        }

        [HttpGet]
        [Route("/clients")]
        public virtual IActionResult GetClientsCount()
        {
            return Ok(_socketRepository.GetAllClients().Count());
        }

        [HttpDelete]
        [Route("/clients")]
        public virtual IActionResult DeleteRegisteredClients()
        {
            _socketRepository.ClearAllClients();
            return Ok(_socketRepository.GetAllClients().Count());
        }
    }
}
