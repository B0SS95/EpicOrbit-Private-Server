using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Shared.ViewModels.Configuration;
using EpicOrbit.Shared.ViewModels.Vault;
using Newtonsoft.Json;

namespace EpicOrbit.Server.Data.ViewModels.Hangar {
    public class HangarView {

        public int ShipID { get; set; }

        public Selection Selection { get; set; }
        public Position Position { get; set; }

        public bool IsCloaked { get; set; }
        public int Hitpoints { get; set; }
        public int MapID { get; set; }
        public bool Configuration { get; set; }

        public int Shield_2 { get; set; }
        public int Shield_1 { get; set; }
        public ConfigurationView Configuration_1 { get; set; }
        public ConfigurationView Configuration_2 { get; set; }

        #region {[ PROPERTIES ]}
        [JsonIgnore] public int MaxHitpoints => Configuration ? Configuration_1.MaxHitpoints : Configuration_2.MaxHitpoints;
        [JsonIgnore] public ConfigurationView CurrentConfiguration => Configuration ? Configuration_1 : Configuration_2;
        [JsonIgnore] public bool LaserEquipped => Configuration ? Configuration_1.LaserEquipped : Configuration_2.LaserEquipped;
        [JsonIgnore] public int LaserEquippedCount => Configuration ? Configuration_1.LaserEquippedCount : Configuration_2.LaserEquippedCount;
        [JsonIgnore] public int MaxShield => Configuration ? Configuration_1.Shield : Configuration_2.Shield;
        [JsonIgnore] public int Damage => Configuration ? Configuration_1.Damage : Configuration_2.Damage;
        [JsonIgnore] public int Speed => Configuration ? Configuration_1.Speed : Configuration_2.Speed;
        [JsonIgnore] public int Shield {
            get => Configuration ? Shield_1 : Shield_2;
            set {
                if (Configuration) {
                    Shield_1 = value;
                } else {
                    Shield_2 = value;
                }
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void Check(IGameLogger logger, int accountId, VaultView vault) {
            Configuration_1.Check(logger, accountId, vault, ShipID.FromShips());
            Configuration_2.Check(logger, accountId, vault, ShipID.FromShips());
        }

        public void Calculate() {
            Ship ship = ShipID.FromShips();

            Configuration_1.Calculate(ship);
            Configuration_2.Calculate(ship);
        }
        #endregion

    }
}
