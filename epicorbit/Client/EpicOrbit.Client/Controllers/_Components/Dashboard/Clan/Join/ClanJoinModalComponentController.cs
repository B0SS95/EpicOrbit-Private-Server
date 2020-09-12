using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Join {
    public class ClanJoinModalComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        [Parameter] protected ClanView Clan { get; set; }
        [Parameter] protected Action Reload { get; set; }

        protected internal string Description { get; set; }

        protected void Confirm() {
            if (!ClanService.CreateJoinRequest(
                new ClanJoinView { ClanID = Clan.ID, Description = Description },
                out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to send join request!");
            } else {
                NotificationService.ShowSuccess("Join request sent!");
                Reload();
            }
        }

    }
}
