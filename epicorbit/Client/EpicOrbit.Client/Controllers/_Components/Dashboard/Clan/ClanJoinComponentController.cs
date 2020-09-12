using EpicOrbit.Client.Pages.Dashboard;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan {
    public class ClanJoinComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        protected internal bool Initialized { get; set; }
        protected internal bool Loading { get; set; } = true;

        protected internal string Query { get; set; }
        protected internal int Page { get; set; } = 1;
        protected internal int TotalPages => (Clans.TotalCount / 25) + 1;
        protected EnumerableResultView<ClanView> Clans { get; set; }

        protected ClanView Selected { get; set; }

        protected override void OnAfterRender() {
            if (!Initialized) {
                Initialized = true;

                Refresh();
                Loading = false;
                StateHasChanged();
            }
        }

        protected void Refresh() {
            if (!ClanService.RetrieveClans(Query, (Page - 1) * 25, 25,
                out EnumerableResultView<ClanView> clans, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to load clans!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Clans = clans;
            }
        }

        protected void ChangePage(int change) {
            Loading = true;
            StateHasChanged();

            Page += change;
            Refresh();

            Loading = false;
            StateHasChanged();
        }

        protected void Revoke(ClanView clanView) {
            Loading = true;
            StateHasChanged();

            if (clanView.Pending) {
                if (!ClanService.RevokeJoinRequest(clanView, out string message, out HttpStatusCode code)) {
                    NotificationService.ShowError(message, "Failed to load revoke join request!");
                    if (code == HttpStatusCode.Unauthorized) {
                        ComponentService.Show(new Login());
                    } else {
                        ComponentService.Show(new CriticalError());
                    }
                } else {
                    NotificationService.ShowSuccess("Join request revoked!");
                    Refresh();
                }
            }

            Loading = false;
            StateHasChanged();
        }

        protected void Change(ClanView clanView) {
            Selected = clanView;
        }

    }
}
