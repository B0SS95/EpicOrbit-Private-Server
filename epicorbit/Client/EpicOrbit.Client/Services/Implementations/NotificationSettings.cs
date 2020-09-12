using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Services.Implementations {
    public class NotificationSettings {

        public NotificationSettings(string heading, string message, string backgroundClass) {
            Heading = heading;
            Message = message;
            BackgroundClass = backgroundClass;
        }

        public string BackgroundClass { get; set; }
        public string Heading { get; set; }
        public string Message { get; set; }

    }
}
