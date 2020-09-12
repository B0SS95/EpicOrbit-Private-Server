using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Configuration {
    public class DroneView {

        #region {[ PROPERTIES ]}
        public int DroneID { get; set; }
        public int StatsDesignID { get; set; }
        public int VisualDesignID { get; set; }
        public int Position { get; set; }
        public List<int> WeaponItems { get; set; } = new List<int>();
        public List<int> ShieldItems { get; set; } = new List<int>();
        #endregion

        #region {[ CONSTRUCTOR ]}
        public DroneView(int droneId, int statsDesignId, int visualDesignId, int position, List<int> weaponItems, List<int> shieldItems) {
            DroneID = droneId;
            StatsDesignID = statsDesignId;
            VisualDesignID = visualDesignId;
            Position = position;
            WeaponItems = weaponItems;
            ShieldItems = shieldItems;
        }
        #endregion

    }
}
