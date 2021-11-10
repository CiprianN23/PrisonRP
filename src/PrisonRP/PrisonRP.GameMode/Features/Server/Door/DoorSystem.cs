using PrisonRP.GameMode.Features.Players.Account;
using PrisonRP.GameMode.Services;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace PrisonRP.GameMode.Features.Server.Door;

public class DoorSystem : ISystem
{
    private readonly List<Door> _doors = new()
    {
        new Door
        {
            Model = 19302,
            IsOpen = false,
            ClosePosition = new Vector3(442.7635, 1562.1702, 1001.2301),
            CloseRotation = new Vector3(0.0, 0.0, 180.0),
            OpenPosition = new Vector3(444.2495, 1562.1702, 1001.2301),
            OpenRotation = new Vector3(0.0, 0.0, 180.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(445.9820, 1494.4187, 1001.2433),
            CloseRotation = new Vector3(0.0, 0.0, -90.0),
            OpenPosition = new Vector3(445.9820, 1493.0267, 1001.2433),
            OpenRotation = new Vector3(0.0, 0.0, -90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(462.0121, 1495.5077, 1001.2293),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(462.0121, 1496.9117, 1001.2293),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(462.0121, 1492.4537, 1001.2293),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(462.0121, 1493.8577, 1001.2293),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(462.0121, 1489.8448, 1001.2293),
            CloseRotation = new Vector3(0.0, 0.0, -90.0),
            OpenPosition = new Vector3(462.0121, 1488.4088, 1001.2293),
            OpenRotation = new Vector3(0.0, 0.0, -90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(462.0121, 1487.4608, 1001.2293),
            CloseRotation = new Vector3(0.0, 0.0, -90.0),
            OpenPosition = new Vector3(462.0121, 1486.0568, 1001.2293),
            OpenRotation = new Vector3(0.0, 0.0, -90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(461.9917, 1498.6859, 1004.7656),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(461.9917, 1500.1929, 1004.7656),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(461.9917, 1493.1759, 1004.7656),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(461.9917, 1494.6139, 1004.7656),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(461.9917, 1487.6239, 1004.7656),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(461.9917, 1489.0879, 1004.7656),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(437.9291, 1624.5547, 1001.2432),
            CloseRotation = new Vector3(0.0, 0.0, 42.0),
            OpenPosition = new Vector3(439.0195, 1625.5581, 1001.2432),
            OpenRotation = new Vector3(0.0, 0.0, 42.0)
        },
        new Door
        {
            Model = 19302,
            IsOpen = false,
            ClosePosition = new Vector3(436.9162, 1655.3450, 1001.2527),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(436.9162, 1653.8390, 1001.2527),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19302,
            IsOpen = false,
            ClosePosition = new Vector3(430.6795, 1600.2559, 1001.2301),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(430.6795, 1598.7889, 1001.2301),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19302,
            IsOpen = false,
            ClosePosition = new Vector3(434.3550, 1600.2580, 1001.230),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(434.3550, 1598.7830, 1001.230),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(197.7702, 1462.9354, 10.8194),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(198.6041, 1462.0718, 10.8194),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 19303,
            IsOpen = false,
            ClosePosition = new Vector3(444.8387, 1525.3693, 1001.2167),
            CloseRotation = new Vector3(0.0, 0.0, -40.0),
            OpenPosition = new Vector3(443.7176, 1526.2751, 1001.2167),
            OpenRotation = new Vector3(0.0, 0.0, -40.0)
        },
        new Door
        {
            Model = 19302,
            IsOpen = false,
            ClosePosition = new Vector3(455.6743, 1525.2963, 997.0681),
            CloseRotation = new Vector3(0.0, 0.0, 90.0),
            OpenPosition = new Vector3(455.6743, 1524.0213, 997.0681),
            OpenRotation = new Vector3(0.0, 0.0, 90.0)
        },
        new Door
        {
            Model = 1495,
            IsOpen = false,
            ClosePosition = new Vector3(436.6110, 1561.6997, 999.9951),
            CloseRotation = new Vector3(0.0, 0.0, 180.0),
            OpenPosition = new Vector3(437.9250, 1561.6997, 999.9951),
            OpenRotation = new Vector3(0.0, 0.0, 180.0)
        },
        new Door
        {
            Model = 1495,
            IsOpen = false,
            ClosePosition = new Vector3(433.6059, 1561.6705, 999.9951),
            CloseRotation = new Vector3(0.0, 0.0, 0.0),
            OpenPosition = new Vector3(432.2829, 1561.6705, 999.9951),
            OpenRotation = new Vector3(0.0, 0.0, 0.0)
        }
    };

    [Event]
    public void OnGameModeInit(IWorldService worldService)
    {
        foreach (var door in _doors)
        {
            door.WorldObject = worldService.CreateObject(door.Model, door.ClosePosition, door.CloseRotation, 0.0f);
        }
    }

    [PlayerCommand]
    public void DoorCommand(PlayerAccountComponent accountComponent, IChatService chatService)
    {
        var player = accountComponent.GetComponent<Player>();
        if (accountComponent.Account.Faction.Id == 1)
        {
            player.SendClientMessage(Color.Gray, "You are not a Prison Guard/Doctor.");
            return;
        }

        var door = _doors.Find(d => d.ClosePosition.DistanceTo(player.Position) <= 2.2f);

        if (door == null)
            return;

        if (door.IsOpen)
        {
            door.WorldObject.Move(door.ClosePosition, 1.5f, door.CloseRotation);
            door.IsOpen = false;
            chatService.SendLocalChatMessage(player, Color.Purple, $"* {player.Name} pushes the door closed and locks it.", 7.0f);
        }
        else
        {
            door.WorldObject.Move(door.OpenPosition, 1.5f, door.OpenRotation);
            door.IsOpen = true;
            chatService.SendLocalChatMessage(player, Color.Purple, $"* {player.Name} pushes the door open.", 7.0f);
        }
    }
}