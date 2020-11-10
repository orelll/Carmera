using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carmera.HttpHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeersController : ControllerBase
    {

        private readonly ILogger<PeersController> _logger;

        public PeersController(ILogger<PeersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/peers")]
        public string GetAll ()
        {
            return "hello world!";
        }

        [HttpPost]
        [Route("/peers")]
        public IEnumerable<object> RegisterPeer([FromBody]string postData)
        {
            return new List<Object> { "Hello world!" };
        }
    }
}
