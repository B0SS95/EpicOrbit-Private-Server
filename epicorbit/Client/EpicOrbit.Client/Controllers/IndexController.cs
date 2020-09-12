using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Dashboard;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers {
    public class IndexController : ComponentBase {

        [Inject] ComponentService ComponentService { get; set; }
        [Inject] AccountService AccountService { get; set; }

        protected RenderFragment Component { get; set; }

        protected override void OnInit() {
            ComponentService.OnShow += Show;

            if (AccountService.IsAuthenticated()) {
                Show(new Pages.Dashboard.Dashboard());
            } else {
                Show(new Login());
            }
        }

        private void Show(IComponent obj) {
            Component = new RenderFragment(b => {
                b.OpenComponent(0, obj.GetType());
                b.CloseComponent();
            });

            StateHasChanged();
        }
    }
}
