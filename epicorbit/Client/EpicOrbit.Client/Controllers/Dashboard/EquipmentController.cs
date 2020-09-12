using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Hangar;
using EpicOrbit.Shared.ViewModels.Vault;
using EpicOrbit.Shared.Items.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers.Dashboard {
    public class EquipmentController : ComponentBase {

        [Inject] HangarService HangarService { get; set; }
        [Inject] AccountService AccountService { get; set; }
        [Inject] StateService StateService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        protected internal bool Initialized { get; set; }
        protected internal bool Loading { get; set; } = true;
        protected internal bool EquipmentDashboardVisible { get; set; }
        protected internal string Padding => "px-lg-5 " + (Loading ? "py-lg-5" : "pb-lg-5");

        protected internal Ship[] Ships { get; set; }
        protected internal Faction Faction { get; set; }
        protected internal Ship Active { get; set; }
        protected internal Ship Selected { get; set; }
        protected internal HangarDetailView Hangar { get; set; }
        protected internal VaultView Vault { get; set; }

        protected override void OnAfterRender() {
            if (!Initialized) {
                Initialized = true;

                Loading = true;
                Refresh();
                Loading = false;
                StateHasChanged();
            }
        }

        protected internal void CloseEquipment() {
            if (EquipmentDashboardVisible) {
                EquipmentDashboardVisible = false;
                StateHasChanged();
            }
        }

        protected internal void SaveEquipment() {
            if (EquipmentDashboardVisible) {
                Loading = true;
                StateHasChanged();

                if (!HangarService.SaveHangar(Hangar, out string message)) {
                    NotificationService.ShowError(message, "Failed to save equipment!");
                } else {
                    NotificationService.ShowSuccess("Equipment saved!");
                }

                Loading = false;
                StateHasChanged();
            }
        }

        protected internal void Equipment(Ship ship) {
            if (!EquipmentDashboardVisible) {
                Loading = true;
                Selected = ship;
                StateHasChanged();

                if (!HangarService.RetrieveHangarDetailView(ship.ID, out HangarDetailView hangarDetailView, out string message)) {
                    NotificationService.ShowError(message, "Failed to load equipment!");
                } else {
                    if (!AccountService.RetrieveAccountVault(out VaultView vaultView, out message, out _)) {
                        NotificationService.ShowError(message, "Failed to load vault!");
                    } else {
                        Hangar = hangarDetailView;
                        Vault = vaultView;
                        EquipmentDashboardVisible = true;
                    }
                }

                Loading = false;
                StateHasChanged();
            }
        }

        protected internal void Activate(Ship ship) {
            if (Active.ID != ship.ID) {
                Loading = true;
                StateHasChanged();

                if (!HangarService.ActivateHangar(ship.ID, out string message)) {
                    NotificationService.ShowError(message, "Failed to activate hangar!");
                } else {
                    NotificationService.ShowSuccess("Hangar activated!");
                    Refresh();
                }

                Loading = false;
                StateHasChanged();
            }
        }

        protected internal void Refresh() {
            if (!StateService.AccountOverview.Retrieve(out AccountOverview account, out string message, out _)) {
                NotificationService.ShowError(message, "Failed to load account!");
                ComponentService.Show(new Login());
                return;
            } else {
                Faction = account.Faction;
            }

            if (!HangarService.RetrieveHangarOverview(out List<HangarOverview> hangarOverview, out message)) {
                NotificationService.ShowError(message, "Failed to load hangar!");
                ComponentService.Show(new Pages.Dashboard.Dashboard());
            } else {
                if (!hangarOverview.Any(x => x.IsActive)) {
                    hangarOverview.First(x => x.ShipID == hangarOverview.Min(y => y.ShipID)).IsActive = true;
                    NotificationService.ShowInfo(message, "Failed to load active ship!");
                }

                Ships = hangarOverview.Select(x => x.ShipID.FromShips()).ToArray();
                Active = hangarOverview.First(x => x.IsActive).ShipID.FromShips();
            }
        }

    }
}
