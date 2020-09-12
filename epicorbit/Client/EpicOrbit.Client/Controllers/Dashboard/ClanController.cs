using EpicOrbit.Client.Pages.Dashboard;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers.Dashboard {
    public class ClanController : ComponentBase {

        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] protected ComponentService ComponentService { get; set; }
        [Inject] protected ClanService ClanService { get; set; }

        protected internal bool Initialized { get; set; }
        protected internal bool Loading { get; set; } = true;
        protected internal bool PlayerIsInClan { get; set; }
        protected internal string Padding => "px-lg-5 " + (Loading ? "py-lg-5" : "pb-lg-3");
        protected internal int View { get; set; }
        protected internal ClanView Current { get; set; }

        protected override void OnAfterRender() {
            if (!Initialized) {
                Initialized = true;

                Refresh();

                Loading = false;
                StateHasChanged();
            }
        }

        protected internal void Reload() {
            Loading = true;
            StateHasChanged();

            ChangeView(0);
            Refresh();

            Loading = false;
            StateHasChanged();
        }

        protected internal void Refresh() {
            if (!ClanService.RetrieveClan(out ClanView clanView, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to load clans!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Current = clanView;
                PlayerIsInClan = clanView != null;
            }
        }

        protected internal void ChangeView(int view) {
            View = view;
            StateHasChanged();
        }

    }
}
