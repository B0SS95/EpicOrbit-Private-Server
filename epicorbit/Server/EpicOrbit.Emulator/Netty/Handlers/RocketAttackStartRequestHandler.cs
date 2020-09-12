using EpicOrbit.Server.Data.Models.Items;
using EpicOrbit.Server.Data.Models.Items.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Assemblies;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class RocketAttackStartRequestHandler : ICommandHandler<RocketAttackStartRequest> {
        public void Execute(IClient initiator, RocketAttackStartRequest command) {

            int dummyLapNumber = 0;
            RocketAmmunition rocket = initiator.Controller.Account.CurrentHangar.Selection.Rocket.FromRocketAmmunitions();
            (initiator.Controller.AttackAssembly as PlayerAttackAssembly).RocketAttack(ref dummyLapNumber, rocket, true);

        }
    }

}
