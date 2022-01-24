using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Models;
using Microsoft.Extensions.Configuration;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
           
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            throw new NotImplementedException();
        }
    }
}