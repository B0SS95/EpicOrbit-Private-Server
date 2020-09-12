using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Navigation {
    public class NavigationComponentController : ComponentBase, IDisposable {

        [Inject] ApiClient ApiClient { get; set; }
        [Inject] StateService StateService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] protected ComponentService ComponentService { get; set; }

        protected AccountPlayersOnline AccountPlayersOnline { get; set; }
        protected string GameToken { get; set; }
        private Timer _timerAccount;

        protected override void OnInit() {
            if (!StateService.AccountPlayersOnline.Retrieve(out AccountPlayersOnline accountPlayersOnline, out string message, out _)) {
                NotificationService.ShowError(message, "Failed to load players online!");
                AccountPlayersOnline = new AccountPlayersOnline();
            } else {
                AccountPlayersOnline = accountPlayersOnline;
            }
            StateService.OnAccountPlayersOnlineRefreshed += OnAccountPlayersOnlineRefreshed;

            GameToken = "/game/" + GenerateGameToken();
            _timerAccount = new Timer((x) => {
                GameToken = "/game/" + GenerateGameToken();
                Invoke(StateHasChanged);
            }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
        }

        public string GenerateGameToken() {
            int displayPreference = 0;
            if (!StateService.AccountOverview.Retrieve(out AccountOverview accountOverview, out _, out _)) {
                NotificationService.ShowWarning("Failed to load preffered gamemode!");
            } else {
                displayPreference = accountOverview.DisplayPreference;
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new AccountGameView {
                ID = ApiClient.AccountSessionView.AccountID,
                Token = ApiClient.AccountSessionView.Token,
                Display = displayPreference
            })));
        }

        public void Dispose() {
            _timerAccount?.Dispose();
        }

        private void OnAccountPlayersOnlineRefreshed(AccountPlayersOnline obj) {
            Invoke(() => {
                AccountPlayersOnline = obj;
                StateHasChanged();
            });
        }

    }
}
