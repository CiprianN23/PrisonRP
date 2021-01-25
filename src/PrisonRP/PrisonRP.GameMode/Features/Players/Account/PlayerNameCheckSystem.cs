using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using System;
using System.Text.RegularExpressions;

namespace PrisonRP.GameMode.Features.Players.Account
{
    public class PlayerNameCheckSystem : ISystem
    {
        [Event]
        public void OnPlayerConnect(Player player, ITimerService timerService)
        {
            player.ToggleSpectating(true);

            if (IsValidRoleplayName(player.Name) == false)
            {
                player.SendClientMessage(Color.Red, "Your name is not a valid roleplay name!");
                player.SendClientMessage(Color.Red, "Please use Firstname_Lastname.");

                TimerReference timer = null;
                timer = timerService.Start(_ =>
                {
                    player.Kick();
                    timerService.Stop(timer);

                }, TimeSpan.FromMilliseconds(500));
            }
        }

        private static bool IsValidRoleplayName(string name)
        {
            var regex = new Regex(@"^[A-Z][a-zA-z]+_[A-Z][a-zA-Z]+$", RegexOptions.Compiled);
            return regex.IsMatch(name);
        }
    }
}
