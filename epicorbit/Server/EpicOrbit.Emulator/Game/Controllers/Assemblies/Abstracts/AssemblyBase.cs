using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using System;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts {
    public abstract class AssemblyBase {

        public EntityControllerBase Controller { get; protected set; }

        public AssemblyBase(EntityControllerBase controller) {
            Controller = controller ?? throw GameContext.Logger.LogError(new ArgumentNullException(nameof(controller)));
        }

        /// <summary>
        /// Push all current changes to the Controller
        /// </summary>
        public abstract void Refresh();

    }
}
