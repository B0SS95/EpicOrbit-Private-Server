using System;
using EpicOrbit.Client.Pages._Components.Notification;
using EpicOrbit.Client.Services.Implementations;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components.Notification {
    public class NotificationComponentController : ComponentBase {

        [Parameter] protected Guid NotificationId { get; set; }
        [Parameter] protected NotificationSettings NotificationSettings { get; set; }
        [CascadingParameter] private NotificationContainerComponent NotificationContainer { get; set; }

        protected void HideNotification() {
            NotificationContainer.RemoveNotification(NotificationId);
        }

    }
}
