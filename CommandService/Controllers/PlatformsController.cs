using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{

    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {

        public PlatformsController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound Post # Command service");
            return Ok("Inbound test ok from Platforms Controller");
        }
        
    }
}