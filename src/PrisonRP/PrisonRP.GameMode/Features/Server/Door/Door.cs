using SampSharp.Entities.SAMP;

namespace PrisonRP.GameMode.Features.Server.Door;

public class Door
{
    public int Model { get; set; }
    public bool IsOpen { get; set; }
    public Vector3 ClosePosition { get; set; }
    public Vector3 CloseRotation { get; set; }
    public Vector3 OpenPosition { get; set; }
    public Vector3 OpenRotation { get; set; }
    public GlobalObject WorldObject { get; set; }
}
