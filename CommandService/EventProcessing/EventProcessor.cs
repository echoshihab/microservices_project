using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public EventProcessor(IServiceScopeFactory scopeFactory, 
        IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlatformPublished: 
                    //TODO
                    break;
                default: 
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch(eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform published event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type.");
                    return EventType.Undetermined;

            }

        
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExists(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exists..");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add platform to db {ex.Message}");
                }
            }

        }


    }

    enum EventType
    {
        PlatformPublished,
        Undetermined

    }
}