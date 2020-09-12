using EpicOrbit.Shared.Items.Interfaces;

namespace EpicOrbit.Shared.Items {
    public sealed class Faction : IIndentifyable {

        #region {[ STATIC ]} // TODO
        public static Faction NONE { get; } = new Faction(0, "");
        public static Faction MMO { get; } = new Faction(1, "MMO");
        public static Faction EIC { get; } = new Faction(2, "EIC");
        public static Faction VRU { get; } = new Faction(3, "VRU");
        #endregion

        #region {[ PROPERTIES ]}
        public int ID { get; }
        public string Name { get; }

      /*  public int LowerBaseMapID { get; }
        public Position LowerBasePosition { get; }
        public Position LowerBaseRepairPosition { get; }

        public int UpperBaseMapID { get; }
        public Position UpperBasePosition { get; }
        public Position UpperBaseRepairPosition { get; } */
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Faction(int id, string name/*, int lowerBaseMapID = 0, Position lowerBasePosition = null, Position lowerBaseRepairPosition = null, int upperBaseMapID = 0, Position upperBasePosition = null, Position upperBaseRepairPosition = null */) {
            ID = id;
            Name = name;
         /*   LowerBaseMapID = lowerBaseMapID;
            LowerBasePosition = lowerBasePosition;
            LowerBaseRepairPosition = lowerBasePosition;

            UpperBaseMapID = upperBaseMapID;
            UpperBasePosition = upperBasePosition;
            UpperBaseRepairPosition = upperBaseRepairPosition; */
        }
        #endregion

    }
}
