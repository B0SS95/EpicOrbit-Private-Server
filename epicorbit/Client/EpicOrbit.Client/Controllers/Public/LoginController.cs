using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Dashboard;
using EpicOrbit.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers.Public {
    public class LoginController : ComponentBase {

        [Inject] protected ComponentService ComponentService { get; set; }
        [Inject] AccountService AccountService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        protected string Username { get; set; }
        protected string Password { get; set; }
        protected bool Loading { get; set; }

        protected void Login() {
            Loading = true;
            StateHasChanged();

            if (!AccountService.Login(Username, Password, out string message)) {
                NotificationService.ShowError(message, "Login failed!");
            } else {
                ComponentService.Show(new Pages.Dashboard.Dashboard());
                NotificationService.ShowSuccess("redirecting ...", "Login succeded!");
            }

            Reset();
        }

        private void Reset() {
            Username = null;
            Password = null;
            Loading = false;
            StateHasChanged();
        }

    }
}
