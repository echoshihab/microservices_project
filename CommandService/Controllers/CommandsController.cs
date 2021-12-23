using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _commandRepo = commandRepo;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!_commandRepo.PlatformExists(platformId))
                return NotFound();
            
            var commands = _commandRepo.GetCommandsForPlatform(platformId);
            
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        
    }
}