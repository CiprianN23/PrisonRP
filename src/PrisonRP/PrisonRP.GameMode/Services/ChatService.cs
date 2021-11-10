using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace PrisonRP.GameMode.Services;

public class ChatService : IChatService
{
    private readonly IEntityManager _entityManager;

    public ChatService(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public void SendLocalChatMessage(Player player, Color color, string message, float radius)
    {
        foreach (var closePlayer in _entityManager.GetComponents<Player>())
        {
            if (player.Position.DistanceTo(closePlayer.Position) <= radius && player.Interior == closePlayer.Interior && player.VirtualWorld == closePlayer.VirtualWorld)
            {
                closePlayer.SendClientMessage(color, message);
            }
        }
    }
}
