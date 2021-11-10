using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using Serilog;

namespace PrisonRP.GameMode.Features.Server;

public class ServerSetupSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.EnableStuntBonus(false);
        serverService.DisableInteriorEnterExits();
        serverService.ShowPlayerMarkers(PlayerMarkersMode.Off);
    }

    [Event]
    public void OnGameModeExit()
    {
        Console.WriteLine("Execute logger flush!");
        Log.CloseAndFlush();
    }
}
