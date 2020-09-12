using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Vault {
    public class VaultView {

        public HashSet<int> Ships { get; set; }
        public HashSet<int> DroneFormations { get; set; }

        public Dictionary<int, int> Drones { get; set; }
        public Dictionary<int, int> DroneDesigns { get; set; }
        public Dictionary<int, int> Generators { get; set; } 
        public Dictionary<int, int> Shields { get; set; }
        public Dictionary<int, int> Weapons { get; set; }
        public Dictionary<int, int> Techs { get; set; }
        public Dictionary<int, TimeSpan> Booster { get; set; }
        public Dictionary<int, int> Ammunitions { get; set; }
        public Dictionary<int, int> RocketAmmunitions { get; set; }
        public Dictionary<int, int> RocketLauncherAmmunitions { get; set; }
        public Dictionary<int, int> Mines { get; set; }
        public Dictionary<int, int> SpecialItems { get; set; }
        public Dictionary<int, int> Extras { get; set; }

    }
}
