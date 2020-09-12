using EpicOrbit.Client.Pages.Dashboard;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan {
    public class ClanDiplomacyComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] StateService StateService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        [Parameter] protected ClanView Clan { get; set; }

        protected bool Initialized { get; set; }
        protected bool Loading { get; set; } = true;

        protected AccountClanView Current { get; set; }
        protected List<ClanDiplomacyView> Diplomacies { get; set; }
        protected List<ClanDiplomacyView> Pending { get; set; }

        protected ClanDiplomacyView Selected { get; set; }
        protected ClanRole Role => Current.Role;

        protected override void OnAfterRender() {
            if (!Initialized) {
                Initialized = true;

                Refresh();
                Loading = false;
                StateHasChanged();
            }
        }

        protected void Change(ClanDiplomacyView selected) {
            Selected = selected;
        }

        protected void Reload() {
            Loading = true;
            StateHasChanged();

            Refresh();

            Loading = false;
            StateHasChanged();
        }

        protected void Refresh() {
            if (!ClanService.RetrieveDiplomacies(out List<ClanDiplomacyView> diplomacies, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to load diplomacies!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Diplomacies = diplomacies;
            }

            if (!ClanService.RetrievePendingDiplomacies(out List<ClanDiplomacyView> pending, out message, out code)) {
                NotificationService.ShowError(message, "Failed to load pending diplomacies!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Pending = pending;
            }

            if (!ClanService.RetrieveSelf(out AccountClanView account, out message, out code)) {
                NotificationService.ShowError(message, "Failed to load self!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Current = account;
            }
        }

    }
}
