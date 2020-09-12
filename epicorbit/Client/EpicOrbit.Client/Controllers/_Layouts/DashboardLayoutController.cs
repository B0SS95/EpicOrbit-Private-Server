using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Layouts {
    public class DashboardLayoutController : ComponentBase {

        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Inject] AccountService AccountService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        protected override void OnInit() {
            if (!AccountService.IsAuthenticated()) {
                ComponentService.Show(new Login());
            }
        }

    }
}
