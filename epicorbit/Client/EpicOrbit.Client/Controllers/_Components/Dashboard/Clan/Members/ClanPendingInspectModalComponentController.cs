using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Members {
    public class ClanPendingInspectModalComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        [Parameter] protected internal AccountClanView Selected { get; set; }
        [Parameter] protected internal Action Reload { get; set; }

        protected void Reject() {
            if (!ClanService.RejectRequest(Selected.ID, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to reject join request!");
            } else {
                NotificationService.ShowSuccess("Request rejected!");
                Reload();
            }
        }

        protected void Accept() {
            if (!ClanService.AcceptRequest(Selected.ID, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to accept join request!");
            } else {
                NotificationService.ShowSuccess("Request accepted!");
                Reload();
            }
        }

    }
}
