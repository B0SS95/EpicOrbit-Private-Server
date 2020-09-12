using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Server.Data.Models.Items.Extensions {
    public static class FactionExtension {

        public static FactionData GetData(this Faction faction) {
            switch (faction.ID) { // TODO
                case 1: return new FactionData(1, new Position(1000, 1000));
                case 2: return new FactionData(1, new Position(1000, 1000));
                case 3: return new FactionData(1, new Position(1000, 1000));
            }
            return new FactionData();
        }

    }
}
