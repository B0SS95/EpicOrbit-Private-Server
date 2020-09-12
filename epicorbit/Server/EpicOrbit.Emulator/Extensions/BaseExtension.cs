using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;
using System.Collections.Generic;

namespace EpicOrbit.Emulator.Extensions {
    public static class BaseExtension {

        public static IEnumerable<AssetObject> From(this BaseObject @base) {
            yield return new AssetObject(new AssetTypeModule(46), @base.OwnerFaction, @base.Position, 0, "HQ");
            yield return new AssetObject(new AssetTypeModule(48), @base.OwnerFaction, new Position(@base.Position.X + 1080, @base.Position.Y), 0, "Hangar");
            yield return new AssetObject(new AssetTypeModule(53), @base.OwnerFaction, new Position(@base.Position.X, @base.Position.Y - 1080), 0, "RepairDock");
            yield return new AssetObject(new AssetTypeModule(50), @base.OwnerFaction, new Position(@base.Position.X - 1080, @base.Position.Y), 0, "OreTrade");

            if (@base.OwnerFaction.ID == Faction.MMO.ID) {
                yield return new AssetObject(new AssetTypeModule(34), Faction.NONE, new Position(@base.Position.X, @base.Position.Y + 1080), 3, "Morgus Petterson");
            } else if (@base.OwnerFaction.ID == Faction.EIC.ID) {
                yield return new AssetObject(new AssetTypeModule(34), Faction.NONE, new Position(@base.Position.X, @base.Position.Y + 1080), 3, "Reginald Crowley");
            } else if (@base.OwnerFaction.ID == Faction.VRU.ID) {
                yield return new AssetObject(new AssetTypeModule(34), Faction.NONE, new Position(@base.Position.X, @base.Position.Y + 1080), 3, "Vanessa Arkadium");
            }

            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1631, @base.Position.Y - 761), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1793, @base.Position.Y - 157), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1738, @base.Position.Y + 465), 0, "StationTurret_Small_1");


            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1474, @base.Position.Y + 1032), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1032, @base.Position.Y + 1474), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 465, @base.Position.Y + 1738), 0, "StationTurret_Small_1");


            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 157, @base.Position.Y + 1793), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 761, @base.Position.Y + 1631), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1273, @base.Position.Y + 1272), 0, "StationTurret_Small_1");


            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1632, @base.Position.Y + 760), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1794, @base.Position.Y + 156), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1739, @base.Position.Y - 466), 0, "StationTurret_Small_1");


            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1475, @base.Position.Y - 1033), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 1033, @base.Position.Y - 1475), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X - 466, @base.Position.Y - 1739), 0, "StationTurret_Small_1");


            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 156, @base.Position.Y - 1794), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 760, @base.Position.Y - 1632), 0, "StationTurret_Small_1");
            yield return new AssetObject(new AssetTypeModule(55), @base.OwnerFaction, new Position(@base.Position.X + 1272, @base.Position.Y - 1273), 0, "StationTurret_Small_1");
        }

    }
}
