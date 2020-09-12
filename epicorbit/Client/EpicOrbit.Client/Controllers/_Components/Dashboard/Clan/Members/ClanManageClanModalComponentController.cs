using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Members {
    public class ClanManageClanModalComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        [Parameter] protected ClanView Clan { get; set; }
        [Parameter] protected Action Reload { get; set; }

        protected string Name { get; set; }
        protected string Tag { get; set; }
        protected string Description { get; set; }

        protected override void OnInit() {
            Name = Clan.Name;
            Tag = Clan.Tag;
            Description = Clan.Description;
        }

        protected void Confirm() {
            if (!ClanService.Edit(new ClanCreateView {
                Name = Name,
                Tag = Tag,
                Description = Description
            }, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to change clan informations!");
            } else {
                NotificationService.ShowSuccess("Clan information changed!");
                Reload();
            }
        }

    }
}
