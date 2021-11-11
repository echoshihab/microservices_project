using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServicess.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);

        

    }
}
