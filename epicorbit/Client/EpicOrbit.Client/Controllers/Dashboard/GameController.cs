using EpicOrbit.Client.Services;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers.Dashboard {
    public class GameController : ComponentBase {

        [Parameter] public string Token { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        protected AccountGameView AccountGameView { get; set; }
        protected bool LoadGame { get; set; } = true;

        protected override void OnInit() {
            AccountGameView = Encoding.UTF8.GetString(Convert.FromBase64String(Token)).DeserializeJsonSafe<AccountGameView>();
            if (AccountGameView == null || DateTime.Now - AccountGameView.CreationDate > TimeSpan.FromMinutes(2)) {
                NotificationService.ShowError("Could not process provided session information", "Invalid session!");
                LoadGame = false;
            }
        }

        protected override void OnAfterRender() {
            if (LoadGame) {
                JSRuntime.InvokeAsync<bool>("StartGameClient", AccountGameView.ID, AccountGameView.Token, AccountGameView.Display)
                    .GetAwaiter()
                    .GetResult();
            }
        }

    }
}
