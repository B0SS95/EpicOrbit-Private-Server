using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Models.Modules {
    public class Selection {

        public int Laser { get; set; }

        public int Rocket { get; set; }

        public int RocketLauncher { get; set; }

        public int Formation { get; set; }

        public int RocketLauncherLoadedCount { get; set; }

        public bool AutoRocketCpu { get; set; }

        public bool AutoRocketLauncherCpu { get; set; }

    }
}
