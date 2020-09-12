using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Shared.ViewModels.Configuration;

namespace EpicOrbit.Server.Data.Models.Modules {
    public class Configuration {

        public List<DroneView> Drones { get; set; } = new List<DroneView>();
        public List<int> Weapons { get; set; } = new List<int>();
        public List<int> Generators { get; set; } = new List<int>();
        public List<int> Shields { get; set; } = new List<int>();

    }
}
