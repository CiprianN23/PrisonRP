using SampSharp.Entities;

namespace PrisonRP.GameMode.Features.Players.Account
{
    public class LoginTriesComponent : Component
    {

        public LoginTriesComponent()
        {
        }

        public int LoginTries { get; set; }
    }
}
