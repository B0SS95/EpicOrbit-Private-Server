using EpicOrbit.Emulator.Game.Objects;
using System.Collections.Generic;
using EpicOrbit.Shared.Items.Interfaces;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Enumerables {
    public sealed class Map : IIndentifyable {

        // Map boundaries 212/132

        #region {[ STATIC ]}
        public static Position DefaultBoundaries = new Position(212, 132);

        public static Map MAP_1_1 { get; } = new Map(1, "1-1", true, false, 1, new List<PortalObject> {
            new PortalObject(new Position(18500, 11500), new Position(0, 0), Faction.MMO, 2)
        }, new List<BaseObject> {
            new BaseObject(new Position(2000, 2000), Faction.MMO)
        }, Faction.MMO, minOtherLevel: 17);

        public static Map MAP_1_2 { get; } = new Map(2, "1-2", false, false, 1, new List<PortalObject> {
            new PortalObject(new Position(1500, 1500),new Position(0, 0), Faction.MMO, 1)
        }, null, Faction.MMO, minOtherLevel: 13);

        public static Map MAP_R_ZONE { get; } = new Map(150, "R-Zone", false, true, 2, new List<PortalObject> {
        }, new List<BaseObject> {
            new BaseObject(new Position(2000, 2000), Faction.MMO),
            new BaseObject(new Position(19000, 2000), Faction.EIC),
            new BaseObject(new Position(10500, 11000), Faction.VRU)
        }, Faction.NONE);
        #endregion

        #region {[ PROPERTIES ]}
        public int ID { get; }
        public string Name { get; }
        public List<PortalObject> Portals { get; }
        public List<BaseObject> Bases { get; }
        public int ViewRange { get; }
        public bool IsStarter { get; }
        public bool IsBattleMap { get; }
        public double ScaleFactor { get; }
        public Position Boundaries { get; }

        public Faction OwnerFaction { get; }
        public int MinimumOwnerFactionLevel { get; }
        public int MinimumOtherFactionLevel { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Map(int id, string name, bool isStarter, bool isBattleMap, double scale, List<PortalObject> portals, List<BaseObject> bases, Faction ownerFaction, int viewRange = 2000, int minOwnerLevel = 0, int minOtherLevel = 0) {
            ID = id;
            Name = name;
            IsStarter = isStarter;
            Portals = portals ?? new List<PortalObject>();
            Bases = bases ?? new List<BaseObject>();
            OwnerFaction = ownerFaction;
            MinimumOwnerFactionLevel = minOwnerLevel;
            MinimumOtherFactionLevel = minOtherLevel;
            ViewRange = viewRange;
            ScaleFactor = scale;
            Boundaries = new Position((int)(DefaultBoundaries.X * scale), (int)(DefaultBoundaries.Y * scale));
            IsBattleMap = isBattleMap;
        }
        #endregion

    }
}
