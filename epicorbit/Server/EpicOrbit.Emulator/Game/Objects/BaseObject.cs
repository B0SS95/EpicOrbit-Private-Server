using EpicOrbit.Emulator.Game.Objects.Abstracts;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Objects {
    public class BaseObject : AddressableObjectBase {

        public Position Position { get; set; }
        public Faction OwnerFaction { get; set; }

        public BaseObject(Position position, Faction ownerFaction) {
            Position = position;
            OwnerFaction = ownerFaction;
        }

    }

}
