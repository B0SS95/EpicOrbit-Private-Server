using EpicOrbit.Client.Services;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Members {
    public class ClanManageMemberModalComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        [Parameter] protected internal AccountClanView Current { get; set; }
        [Parameter] protected internal AccountClanView Selected { get; set; }
        [Parameter] protected internal Action Reload { get; set; }

        protected internal ClanRole Role { get; set; }

        private int Old { get; set; }

        protected override void OnAfterRender() {
            if (Selected != null && Selected.ID != Old) {
                Old = Selected.ID;
                Role = Selected.Role;
            }
        }

        protected void Select(ClanRole role) {
            if (role == ClanRole.LEADER && Role != role) {
                NotificationService.ShowWarning("You will loose your role as leader as soon as you assign it!");
            }
            Role = role;
        }

        protected void Confirm() {
            if (!ClanService.AssignRole(Selected.ID, (int)Role, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to assign role!");
            } else {
                NotificationService.ShowSuccess("Role assigned!");
                Reload();
            }
        }

    }
}
