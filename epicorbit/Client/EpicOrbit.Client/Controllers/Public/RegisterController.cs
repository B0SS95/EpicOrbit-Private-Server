using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers.Public {
    public class RegisterController : ComponentBase {

        [Inject] protected ComponentService ComponentService { get; set; }
        [Inject] AccountService AccountService { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        protected string Username { get; set; }
        protected string Password { get; set; }
        protected string PasswordConfirmation { get; set; }
        protected string Email { get; set; }

        protected bool FactionChooseVisible { get; set; }
        protected bool Loading { get; set; }

        protected void Register(int faction) {
            Loading = true;
            StateHasChanged();

            if (Password != PasswordConfirmation) {
                NotificationService.ShowError("Password and Password Confirmation do not match", "Register failed!");
            } else {
                if (!AccountService.Register(Username, Password, Email, faction, out string message)) {
                    NotificationService.ShowError(message, "Register failed!");
                } else {
                    ComponentService.Show(new Login());
                    NotificationService.ShowSuccess("Register succeded! Please verify your e-mail address first!", "Register succeeded!");
                }
            }
            
            Reset();
        }

        protected void ToggleFactionView() {
            FactionChooseVisible = !FactionChooseVisible;
            StateHasChanged(); 
        }

        private void Reset() {
            Password = null;
            PasswordConfirmation = null;
            Loading = false;
            FactionChooseVisible = false;
            StateHasChanged();
        }

    }
}
