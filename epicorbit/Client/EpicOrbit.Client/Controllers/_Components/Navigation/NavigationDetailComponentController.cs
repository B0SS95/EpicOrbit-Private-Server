using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components.Navigation {
    public class NavigationDetailComponentController : ComponentBase {

        [Inject] NotificationService NotificationService { get; set; }
        [Inject] StateService StateService { get; set; }
        [Inject] ComponentService ComponentService { get; set; }

        protected AccountOverview Account { get; set; }

        protected override void OnInit() {
            if (!StateService.AccountOverview.Retrieve(out AccountOverview account, out string message, out _)) {
                NotificationService.ShowError(message, "Failed to load account!");
                ComponentService.Show(new Login());
            } else {
                Account = account;
            }

            StateService.OnAccountOverviewRefreshed += OnAccountOverviewRefreshed;
        }

       

        private void OnAccountOverviewRefreshed(AccountOverview obj) {
            Invoke(() => {
                Account = obj;
                StateHasChanged();
            });
        }
    }
}
