using EpicOrbit.Emulator.Game.Objects.Abstracts;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Objects {
    public class PortalObject : AddressableObjectBase {

        #region {[ PROPERTIES ]}
        public Position Position { get; }
        public Position DestinationPosition { get; }
        public int DestinationMapID { get; }
        public Faction OwnerFaction { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PortalObject(Position position, Position destinationPosition, Faction ownerFaction, int destination) {
            Position = position;
            DestinationPosition = destinationPosition;
            OwnerFaction = ownerFaction;
            DestinationMapID = destination;
        }
        #endregion

    }
}
