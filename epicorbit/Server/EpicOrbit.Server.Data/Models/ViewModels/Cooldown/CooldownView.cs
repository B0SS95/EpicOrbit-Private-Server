using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Models.ViewModels.Cooldown {
    public class CooldownView {

        public DateTime LastMine { get; set; }
        public DateTime RocketLauncherLastFire { get; set; }
        public DateTime InfectionUntil { get; set; }

        public Dictionary<int, DateTime> TechCooldown { get; set; } = new Dictionary<int, DateTime>();
        public Dictionary<int, DateTime> AbilityCooldown { get; set; } = new Dictionary<int, DateTime>();
        public Dictionary<int, DateTime> SpecialItemCooldown { get; set; } = new Dictionary<int, DateTime>();

    }
}
