using SampSharp.Entities.SAMP;

namespace PrisonRP.GameMode.Services;

public interface IChatService
{
    void SendLocalChatMessage(Player player, Color color, string message, float radius);
}
