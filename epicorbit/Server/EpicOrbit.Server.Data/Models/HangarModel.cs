using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Models.Items.Extensions;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Repositories.Attributes;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Server.Data.Models {
    public class HangarModel : ModelBase {

        public HangarModel() { }
        public HangarModel(int id, int shipId, int faction) {
            AccountID = id;
            ShipID = shipId;

            var factionData = faction.FromFactions().GetData();
            MapID = factionData.LowerBaseMapID;
            Position = factionData.LowerBasePosition;
        }

        [CompoundIndex(1)] public int AccountID { get; set; }
        [CompoundIndex(1)] public int ShipID { get; set; }

        public Selection Selection { get; set; } = new Selection();
        public Position Position { get; set; }

        public bool IsCloaked { get; set; }
        public int Hitpoints { get; set; } = 1000;
        public int MapID { get; set; }
        public bool Configuration { get; set; }

        public int Shield_2 { get; set; }
        public int Shield_1 { get; set; }

        public Configuration Configuration_1 { get; set; } = new Configuration();
        public Configuration Configuration_2 { get; set; } = new Configuration();

    }
}
