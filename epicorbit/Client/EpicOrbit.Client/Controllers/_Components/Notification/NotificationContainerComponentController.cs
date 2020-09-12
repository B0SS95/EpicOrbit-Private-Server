using System;
using System.Collections.Generic;
using System.Timers;
using EpicOrbit.Client.Pages._Components;
using EpicOrbit.Client.Pages._Components.Notification;
using EpicOrbit.Client.Services;
using EpicOrbit.Client.Services.Enumerables;
using EpicOrbit.Client.Services.Implementations;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components.Notification {
    public class NotificationContainerComponentController : ComponentBase {

        [Inject] NotificationService NotificationService { get; set; }
        protected Dictionary<Guid, RenderFragment> NotificationList { get; set; } = new Dictionary<Guid, RenderFragment>();

        protected override void OnInit() {
            NotificationService.OnShow += ShowNotification;
        }

        private void ShowNotification(NotificationLevel level, string message, string heading) {
            var settings = new NotificationSettings(string.IsNullOrWhiteSpace(heading) ? level.ToString("G") : heading, message, $"notification-{level.ToString("G").ToLower()}");
            var notificationId = Guid.NewGuid();
            var notification = new RenderFragment(b => {
                b.OpenComponent<NotificationComponent>(0);
                b.AddAttribute(1, "NotificationSettings", settings);
                b.AddAttribute(2, "NotificationId", notificationId);
                b.CloseComponent();
            });

            NotificationList.Add(notificationId, notification);

            var notificationTimer = new Timer(5000);
            notificationTimer.Elapsed += (sender, args) => { RemoveNotification(notificationId); };
            notificationTimer.AutoReset = false;
            notificationTimer.Start();

            StateHasChanged();
        }

        public void RemoveNotification(Guid notificationId) {
            Invoke(() => {
                NotificationList.Remove(notificationId);
                StateHasChanged();
            });
        }

    }
}
