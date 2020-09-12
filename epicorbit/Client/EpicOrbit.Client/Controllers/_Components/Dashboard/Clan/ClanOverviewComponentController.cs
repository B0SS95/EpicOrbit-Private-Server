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

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan {
    public class ClanOverviewComponentController : ComponentBase {

        [Inject] protected ComponentService ComponentService { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] protected ClanService ClanService { get; set; }

        [Parameter] protected internal ClanView Clan { get; set; }

    }
}
