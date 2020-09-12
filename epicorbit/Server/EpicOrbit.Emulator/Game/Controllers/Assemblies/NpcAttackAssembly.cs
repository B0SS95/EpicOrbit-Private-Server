using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class NpcAttackAssembly : AttackAssemblyBase {
        public NpcAttackAssembly(EntityControllerBase controller) : base(controller) {
        }

        public override void Start() {
            //     throw new NotImplementedException();
        }

        public override void Stop() {
            //   throw new NotImplementedException();
        }

        protected override void AttackTimer() {
            // throw new NotImplementedException();
        }

        protected override void Tick(double changeSinceLastTime) {
            //throw new NotImplementedException();
        }
    }
}
