using EpicOrbit.Shared.ViewModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Hangar {
    public class HangarDetailView {

        public int ShipID { get; set; }
        public int MapID { get; set; }
        public bool IsActive { get; set; }

        public ConfigurationView Configuration_1 { get; set; }
        public ConfigurationView Configuration_2 { get; set; }

    }
}
