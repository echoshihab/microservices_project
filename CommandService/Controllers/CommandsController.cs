using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
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

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} /{commandId}");

            if(!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _commandRepo.GetCommand(platformId, commandId);

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));

        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
          Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

          if(!_commandRepo.PlatformExists(platformId))
          {
              return NotFound();
          }   

          var command = _mapper.Map<Command>(commandDto);

          _commandRepo.CreateCommand(platformId, command);
          _commandRepo.SaveChanges();

          var commandReadDto = _mapper.Map<CommandReadDto>(command);

          return CreatedAtRoute(nameof(GetCommandsForPlatform), 
                new {platformId = platformId, commandId = commandReadDto }, commandReadDto);
        }

        
    }
}