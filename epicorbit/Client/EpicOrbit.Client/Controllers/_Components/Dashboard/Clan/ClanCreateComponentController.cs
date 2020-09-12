using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan {
    public class ClanCreateComponentController : ComponentBase {

        [Inject] ClanService ClanService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        [Parameter] Action Reload { get; set; }

        protected internal bool Loading { get; set; }
        protected internal string Name { get; set; }
        protected internal string Tag { get; set; }
        protected internal string Description { get; set; }

        protected void Create() {
            Loading = true;
            StateHasChanged();

            if (!ClanService.CreateClan(new ClanCreateView {
                Tag = Tag,
                Name = Name,
                Description = Description
            }, out string message, out HttpStatusCode code)) {
                NotificationService.ShowError(message, "Failed to create clan!");
                if (code == HttpStatusCode.Unauthorized) {
                    ComponentService.Show(new Login());
                } else {
                    Loading = false;
                }
            } else {
                NotificationService.ShowSuccess("Clan created!");
                Reload();
            }

            StateHasChanged();
        }

    }
}
