using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Extensions {
    public static class TimeExtension {

        public static TimeSpan FromNow(this DateTime pastEvent) {
            return DateTime.Now - pastEvent;
        }

        public static long FromNow(this long pastTick, long currentTick) {
            return currentTick - pastTick;
        }

    }
}
