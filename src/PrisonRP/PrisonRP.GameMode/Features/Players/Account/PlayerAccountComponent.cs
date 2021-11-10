using PrisonRP.Data.Models;
using SampSharp.Entities;

namespace PrisonRP.GameMode.Features.Players.Account;

public class PlayerAccountComponent : Component
{
    public PlayerAccountComponent(User playerAccount)
    {
        Account = playerAccount;
    }
    public User Account { get; set; }
}
