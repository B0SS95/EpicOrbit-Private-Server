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
    public class ClanMembersComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] StateService StateService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        [Parameter] protected Action ReloadP { get; set; }
        [Parameter] protected ClanView Clan { get; set; }

        protected internal bool Initialized { get; set; }
        protected internal bool Loading { get; set; } = true;
        protected internal List<AccountClanView> Members { get; set; }
        protected internal List<AccountClanView> Pending { get; set; }
        protected internal AccountClanView Current { get; set; }

        protected internal AccountClanView Selected { get; set; }
        protected internal ClanRole Role => Current.Role;

        protected override void OnAfterRender() {
            if (!Initialized) {
                Initialized = true;

                Refresh();
                Loading = false;
                StateHasChanged();
            }
        }

        protected void Kick(AccountClanView selected) {

        }

        protected void Change(AccountClanView selected) {
            Selected = selected;
        }

        protected void Reload() {
            Loading = true;
            StateHasChanged();

            Refresh();

            Loading = false;
            StateHasChanged();
        }

        protected void Leave() {
            if (!ClanService.Leave(out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to leave clan!");
            } else {
                NotificationService.ShowSuccess("Clan left!");
                ReloadP();
            }
        }

        protected void Refresh() {
            if (!ClanService.RetrieveMembers(out List<AccountClanView> members, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to load members!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    ComponentService.Show(new CriticalError());
                }
            } else {
                Members = members;
            }

            if (!ClanService.RetrievePending(out List<AccountClanView> pending, out message, out code)) {
                NotificationService.ShowError(message, "Failed to load pending members!");
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
