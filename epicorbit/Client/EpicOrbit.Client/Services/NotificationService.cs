using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using EpicOrbit.Client.Services.Enumerables;

namespace EpicOrbit.Client.Services {
    public class NotificationService {

        #region {[ EVENTS ]}
        public event Action<NotificationLevel, string, string> OnShow;
        #endregion

        #region {[ FUNCTIONS ]}
        public void ShowNotification(NotificationLevel level, string message, string heading = "") {
            OnShow?.Invoke(level, message, heading);
        }

        public void ShowInfo(string message, string heading = "") {
            ShowNotification(NotificationLevel.Info, message, heading);
        }

        public void ShowSuccess(string message, string heading = "") {
            ShowNotification(NotificationLevel.Success, message, heading);
        }

        public void ShowWarning(string message, string heading = "") {
            ShowNotification(NotificationLevel.Warning, message, heading);
        }

        public void ShowError(string message, string heading = "") {
            ShowNotification(NotificationLevel.Error, message, heading);
        }
        #endregion

    }
}
