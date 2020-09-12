using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerGroupAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; set; }
        public bool AcceptInvitations { get; set; } = true;
        public GroupController Group { get; set; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerGroupAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
        }
        #endregion

        public override void Refresh() { }
    }
}
