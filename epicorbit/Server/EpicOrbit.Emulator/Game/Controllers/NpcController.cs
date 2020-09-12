using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Shared.Items;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Emulator.Game.Controllers {
    public class NpcController : EntityControllerBase {

        public NpcController(int id, string username, Faction faction) : base(id, username, faction) {
            BoosterAssembly = new BoosterAssembly(this);
            HangarAssembly = new NpcHangarAssembly(this, Ship.YAMATO, Map.MAP_R_ZONE, new Position(10000, 6000), 1_000_000, 1_000_000);
            MovementAssembly = new MovementAssembly(this);
            AttackAssembly = new NpcAttackAssembly(this);
            EffectsAssembly = new EffectsAssembly(this);
            AttackTraceAssembly = new AttackTraceAssembly(this);
            ZoneAssembly = new ZoneAssembly(this);

            BoosterAssembly.Set(BoosterType.SHIELD_REGNERATION, 0.05);
            BoosterAssembly.Set(BoosterType.SHIELD_ABSORBATION, 0.5);
            BoosterAssembly.Set(BoosterType.HITPOINTS_REGENERATION, 0.01);

            TimerStart();
            InitializeTimer();

            SpacemapController.For(HangarAssembly.Map.ID).Add(this);
        }

        public override async void Die() {
            Lock(null);
            EntitiesLockedSafe(x => {
                if (x.ID == AttackTraceAssembly.CurrentMainAttacker
                    && x is PlayerController killer) {
                    killer.PlayerItemsAssembly.ChangeUridium(5000);
                    killer.PlayerItemsAssembly.ChangeExperience(500 * 1024, false, true);
                    killer.PlayerItemsAssembly.ChangeHonor(5000);
                }

                if (x.Locked != null && x.Locked.ID == ID) {
                    x.Lock(null);
                }
            });

            ICommand killCommand = PacketBuilder.KillCommand(this);
            EntitesInRange(x => x.Send(killCommand));
            Spacemap?.Remove(this); // remove from spacemap

            TimerStop();

            await Task.Delay(3000);
            new NpcController(ID + 1, "Ehrenhaftes NPC " + (ID + 1), Faction.NONE);
        }

        public override void Dispose() { }

        public override void EntityAddedToMap(EntityControllerBase entity) { }

        public override void EntityRemovedFromMap(int id) { }

        public override void Refresh() { }

        public override void Send(ICommand command) { }

        public override void Send(params ICommand[] commands) { }

        public override void Send(IEnumerable<ICommand> commands) { }

    }
}
