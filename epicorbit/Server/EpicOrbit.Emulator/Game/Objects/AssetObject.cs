using EpicOrbit.Server.Data.Models.Items;
using EpicOrbit.Emulator.Game.Objects.Abstracts;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Objects {
    public class AssetObject : AddressableObjectBase {

        #region {[ PROPERTIES ]}
        public AssetTypeModule Type { get; }
        public Faction OwnerFaction { get; }
        public Position Position { get; }

        public int DesignID { get; }
        public string Username { get; }
        #endregion

        #region {[ FIELDS ]}
        #endregion

        #region {[ CONSTRUCTOR ]}
        public AssetObject(AssetTypeModule type, Faction owner, Position position, int design, string username) {
            Type = type;
            OwnerFaction = owner;
            Position = position;
            DesignID = design;
            Username = username;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public virtual ICommand Render() {
            return new AssetCreateCommand(Type, Username, OwnerFaction.ID, "", ID, DesignID, 0, Position.X, Position.Y,
                0, false, true, true, false);
        }
        #endregion

    }
}
