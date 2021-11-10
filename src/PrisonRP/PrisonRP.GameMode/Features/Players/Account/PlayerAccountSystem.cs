using Microsoft.EntityFrameworkCore;
using PrisonRP.Data;
using PrisonRP.Data.Models;
using PrisonRP.GameMode.Utilities;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace PrisonRP.GameMode.Features.Players.Account;

public class PlayerAccountSystem : ISystem
{
    private readonly IDialogService _dialogService;
    private readonly ITimerService _timerService;

    public PlayerAccountSystem(IDialogService dialogService, ITimerService timerService)
    {
        _dialogService = dialogService;
        _timerService = timerService;
    }

    [Event]
    public void OnPlayerConnect(Player player, ApplicationContext prisonContext)
    {
        TimerReference? timer = null;
        timer = _timerService.Start(_ =>
        {
            LoginOrRegisterUser(player, prisonContext);
            _timerService.Stop(timer);
        }, TimeSpan.FromSeconds(1));
    }

    private void LoginOrRegisterUser(Player player, ApplicationContext prisonContext)
    {
        if (!player.IsComponentAlive)
            return;

        var playerAccountDb = player.AddComponent<PlayerAccountComponent>(prisonContext.Users.Include(y => y.Faction).FirstOrDefault(x => x.Name == player.Name));
        if (playerAccountDb.Account is null)
        {
            var registerDialog = new InputDialog() { IsPassword = true, Caption = "Register", Content = "Input your password below to register a new account.", Button1 = "Register", Button2 = "Exit" };

            void RegisterDialogHandler(InputDialogResponse r)
            {
                if (r.Response == DialogResponse.LeftButton)
                {
                    if (string.IsNullOrEmpty(r.InputText))
                    {
                        player.SendClientMessage(Color.Red, "Password can't be empty.");
                        _dialogService.Show(player.Entity, registerDialog, RegisterDialogHandler);
                        return;
                    }

                    if (r.InputText.Length < 8)
                    {
                        player.SendClientMessage(Color.Red, "Password must be at least 8 characters long.");
                        _dialogService.Show(player.Entity, registerDialog, RegisterDialogHandler);
                        return;
                    }

                    var playerAccount = new User() { Name = player.Name, Password = BCrypt.Net.BCrypt.EnhancedHashPassword(r.InputText), Faction = prisonContext.Factions.Find(1) };
                    var databaseAccount = prisonContext.Users.Add(playerAccount);
                    player.GetComponent<PlayerAccountComponent>().Account = databaseAccount.Entity;
                    prisonContext.SaveChanges();

                    player.SendClientMessage(Color.Gray, "You have successfully created an account and auto logged in.");
                    player.AddComponent<IsLoggedInComponent>();

                    ShowSelectGenderDialog(player, prisonContext);
                }
                else if (r.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.Kick();
                }
            }

            _dialogService.Show(player.Entity, registerDialog, RegisterDialogHandler);
        }
        else
        {
            var playerLoginTries = player.AddComponent<LoginTriesComponent>();

            var loginDialog = new InputDialog() { IsPassword = true, Caption = "Login", Content = "Input your password below to login.", Button1 = "Login", Button2 = "Exit" };

            void LoginDialogHandler(InputDialogResponse response)
            {
                if (response.Response == DialogResponse.LeftButton)
                {
                    if (!BCrypt.Net.BCrypt.EnhancedVerify(response.InputText, playerAccountDb.Account.Password))
                    {
                        playerLoginTries.LoginTries++;

                        if (playerLoginTries.LoginTries >= 5)
                        {
                            player.SendClientMessage(Color.Red, "You exceeded the numbeer of allowed logins. You can try again in 24 hours.");

                            TimerReference? timer = null;
                            timer = _timerService.Start(_ =>
                            {
                                // TODO: Add temporary ban for 24h
                                player.Kick();
                                _timerService.Stop(timer);
                            }, TimeSpan.FromSeconds(1));
                        }

                        _dialogService.Show(player.Entity, loginDialog, LoginDialogHandler);
                    }
                    else
                    {
                        player.DestroyComponents<LoginTriesComponent>();
                        player.AddComponent<IsLoggedInComponent>();
                        SpawnPlayer(player);
                    }
                }
                else if (response.Response == DialogResponse.RightButtonOrCancel)
                {
                    player.Kick();
                }
            }

            _dialogService.Show(player.Entity, loginDialog, LoginDialogHandler);
        }
    }

    private void ShowSelectGenderDialog(Player player, ApplicationContext prisonContext)
    {
        var genderDialog = new ListDialog("Are you a male or a female?", "Next", "")
            {
                "Female",
                "Male"
            };

        void GenderDialogHandler(ListDialogResponse r)
        {
            if (r.Response == DialogResponse.RightButtonOrCancel)
            {
                _dialogService.Show(player.Entity, genderDialog, GenderDialogHandler);
                return;
            }
            else
            {
                var playerAccount = player.GetComponent<PlayerAccountComponent>();

                if (r.ItemIndex == 0)
                {
                    playerAccount.Account.LastSkin = 191;
                    player.SendClientMessage(Color.Gray, "So, you are a female.");
                }
                else
                {
                    playerAccount.Account.LastSkin = 50;
                    player.SendClientMessage(Color.Gray, "So, you are a male.");
                }

                playerAccount.Account.Gender = r.ItemIndex;
                prisonContext.SaveChanges();

                ShowAgeInputDialog(player, prisonContext);
            }
        }

        _dialogService.Show(player.Entity, genderDialog, GenderDialogHandler);
    }

    private void ShowAgeInputDialog(Player player, ApplicationContext prisonContext)
    {
        var ageDialog = new InputDialog() { Caption = "You need to type your age and press on 'Next'", Content = "Input your age below.", Button1 = "Next", Button2 = "" };

        void AgeDialogHandlerAsync(InputDialogResponse r)
        {
            if (!InputValidation.IsInputValidAge(r.InputText))
            {
                player.SendClientMessage(Color.Gray, "Your age must be a valid number between 18 and 70.");
                _dialogService.Show(player.Entity, ageDialog, AgeDialogHandlerAsync);
            }
            else
            {
                var playerAccount = player.GetComponent<PlayerAccountComponent>();
                playerAccount.Account.Age = int.Parse(r.InputText);
                prisonContext.SaveChanges();

                player.SendClientMessage(Color.Gray, $"And you are {r.InputText}. Why are are you in prison?");
                ShowReasonDialog(player, prisonContext);
            }
        }

        _dialogService.Show(player.Entity, ageDialog, AgeDialogHandlerAsync);
    }

    private void ShowReasonDialog(Player player, ApplicationContext prisonContext)
    {
        var reasonDialog = new InputDialog() { Caption = "Why were you imprisoned?", Content = "Type the reason below and press on Done.", Button1 = "Done", Button2 = "" };

        void ReasonDialogHandler(InputDialogResponse r)
        {
            if (string.IsNullOrEmpty(r.InputText))
            {
                player.SendClientMessage(Color.Gray, "You must enter the reason below.");
                _dialogService.Show(player.Entity, reasonDialog, ReasonDialogHandler);
                return;
            }

            if (r.InputText.Length < 4 && r.InputText.Length > 20)
            {
                player.SendClientMessage(Color.Gray, "Reason must be between 4 and 20 characters.");
                _dialogService.Show(player.Entity, reasonDialog, ReasonDialogHandler);
                return;
            }

            var playerAccount = player.GetComponent<PlayerAccountComponent>();
            playerAccount.Account.ImprisonmentReason = r.InputText;
            prisonContext.SaveChanges();

            SpawnPlayer(player);
        }

        _dialogService.Show(player.Entity, reasonDialog, ReasonDialogHandler);
    }

    private void SpawnPlayer(Player player)
    {
        var playerAccount = player.GetComponent<PlayerAccountComponent>().Account;

        player.ToggleSpectating(false);
        player.SetSpawnInfo(0, playerAccount.LastSkin, new Vector3(playerAccount.LastPositionX, playerAccount.LastPositionY, playerAccount.LastPositionZ), playerAccount.LastPositionAngle);
        player.Spawn();
        player.ToggleControllable(false);

        TimerReference? timer = null;
        timer = _timerService.Start(_ =>
        {
            player.ToggleControllable(true);
            _timerService.Stop(timer);
        }, TimeSpan.FromSeconds(3));

        player.ResetMoney();
        player.GiveMoney(playerAccount.Money);
        player.Interior = playerAccount.LastInterior;
        player.VirtualWorld = playerAccount.LastWorld;
        player.Health = playerAccount.Health;
        player.Armour = playerAccount.Armour;

        player.PutCameraBehindPlayer();
        player.FightStyle = (FightStyle)playerAccount.FightStyle;
    }

    [PlayerCommand]
    public void ChangePasswordCommand(IsLoggedInComponent isLoggedInComponent, ApplicationContext prisonContext)
    {
        var player = isLoggedInComponent.GetComponent<Player>();
        var oldPaasswordDialog = new InputDialog() { IsPassword = true, Caption = "Change password", Content = "Please input below your old password", Button1 = "Accept", Button2 = "Close" };

        void OldPasswordDialogHandler(InputDialogResponse oldPasswordResponse)
        {
            if (oldPasswordResponse.Response == DialogResponse.RightButtonOrCancel)
                return;

            if (string.IsNullOrEmpty(oldPasswordResponse.InputText))
            {
                _dialogService.Show(player.Entity, oldPaasswordDialog, OldPasswordDialogHandler);
                return;
            }

            var playerAccount = player.GetComponent<PlayerAccountComponent>().Account;

            if (!BCrypt.Net.BCrypt.EnhancedVerify(oldPasswordResponse.InputText, playerAccount.Password))
            {
                player.SendClientMessage(Color.Red, "Password don't match. Try again!");
                _dialogService.Show(player.Entity, oldPaasswordDialog, OldPasswordDialogHandler);
                return;
            }

            var newPasswordDialog = new InputDialog() { IsPassword = true, Caption = "Change password", Content = "Please input below your new password", Button1 = "Accept", Button2 = "Close" };

            void NewPasswordDialogHandlerAsync(InputDialogResponse newPasswordResponse)
            {
                if (string.IsNullOrEmpty(newPasswordResponse.InputText))
                {
                    player.SendClientMessage(Color.Red, "Password can't be empty.");
                    _dialogService.Show(player.Entity, newPasswordDialog, NewPasswordDialogHandlerAsync);
                    return;
                }

                if (newPasswordResponse.InputText.Length < 8)
                {
                    player.SendClientMessage(Color.Red, "Password must be at least 8 characters long.");
                    _dialogService.Show(player.Entity, newPasswordDialog, NewPasswordDialogHandlerAsync);
                    return;
                }

                playerAccount.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPasswordResponse.InputText);
                prisonContext.SaveChanges();

                player.SendClientMessage(Color.GreenYellow, "Your password successfully changed!");
            }

            _dialogService.Show(player.Entity, newPasswordDialog, NewPasswordDialogHandlerAsync);
        }

        _dialogService.Show(player.Entity, oldPaasswordDialog, OldPasswordDialogHandler);
    }
}
