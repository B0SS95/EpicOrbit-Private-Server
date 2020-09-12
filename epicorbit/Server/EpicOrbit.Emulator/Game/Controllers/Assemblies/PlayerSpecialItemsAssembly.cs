using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerSpecialItemsAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; protected set; }
        public long NotTargetableUntil { get; protected set; }
        public long InvincibleUntil { get; protected set; }

        public bool IsNotTargetable => NotTargetableUntil >= PlayerController.CurrentClock.ElapsedMilliseconds;
        public bool IsInvicible => InvincibleUntil >= PlayerController.CurrentClock.ElapsedMilliseconds;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerSpecialItemsAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void TriggerSMB() {
            SpecialItem item = SpecialItem.SMB_01;

            if (!PlayerController.Account.Vault.SpecialItems.TryGetValue(item.ID, out int currentCount)
                || currentCount <= 0) {
                return;
            }
            PlayerController.Account.Vault.SpecialItems[item.ID] = --currentCount;

            PlayerController.Account.Cooldown.SpecialItemCooldown.TryGetValue(item.ID, out DateTime lastTrigger);
            if (lastTrigger.FromNow() < item.Cooldown) {
                return;
            }

            PlayerController.Account.Cooldown.SpecialItemCooldown[item.ID] = DateTime.Now;

            ICommand smbCommand = PacketBuilder.SpecialItems.Smartbomb(PlayerController);
            PlayerController.Send(
                PacketBuilder.Slotbar.ExplosiveItemStatus(item.Name, currentCount, false),
                PacketBuilder.Slotbar.ItemCooldownCommand(item.Name, item.Cooldown.TotalMilliseconds, currentCount > 0),
                smbCommand
            );

            PlayerController.EntitiesInRangeSafe(x => { // if performance does not meet demand, add async here
                if (x is PlayerController playerController && !playerController.SpecialItemsAssembly.IsInvicible
                    && !x.EffectsAssembly.HasProtection && !x.ZoneAssembly.IsInDMZ
                    && x.MovementAssembly.ActualPosition().DistanceTo(PlayerController.MovementAssembly.ActualPosition()) < 550) {

                    double damage = x.HangarAssembly.Hitpoints * .2;
                    int shieldDamage = Math.Abs(x.HangarAssembly.ChangeShield(-(int)(damage * x.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION)), false));
                    int hitpointsDamage = Math.Abs(x.HangarAssembly.ChangeHitpoints(-(int)(damage - shieldDamage), false));

                    x.AttackTraceAssembly.LogAttack(PlayerController, shieldDamage, hitpointsDamage, false);
                    x.HangarAssembly.CheckDeath();

                    ICommand attackCommand = PacketBuilder.AttackCommand(PlayerController, x, AttackTypeModule.SMARTBOMB, shieldDamage + hitpointsDamage);
                    x.Send(smbCommand, attackCommand); // send to player
                    x.EntitiesLocked(y => y.Send(attackCommand)); // send to all who have him in lock

                } else {
                    x.Send(smbCommand);
                }
            });
        }

        public void TriggerEMP() {
            SpecialItem item = SpecialItem.EMP_01;

            if (!PlayerController.Account.Vault.SpecialItems.TryGetValue(item.ID, out int currentCount)
                || currentCount <= 0) {
                return;
            }
            PlayerController.Account.Vault.SpecialItems[item.ID] = --currentCount;

            PlayerController.Account.Cooldown.SpecialItemCooldown.TryGetValue(item.ID, out DateTime lastTrigger);
            if (lastTrigger.FromNow() < item.Cooldown) {
                return;
            }

            PlayerController.Account.Cooldown.SpecialItemCooldown[item.ID] = DateTime.Now;

            ICommand empCommand = PacketBuilder.SpecialItems.EMP(PlayerController);
            ICommand noiseCommand = PacketBuilder.MapNoiseCommand();
            ICommand targetingHarmed = PacketBuilder.Messages.TargetingHarmed();

            PlayerController.Send(
                PacketBuilder.Slotbar.ExplosiveItemStatus(item.Name, currentCount, false),
                PacketBuilder.Slotbar.ItemCooldownCommand(item.Name, item.Cooldown.TotalMilliseconds, currentCount > 0),
                empCommand,
                noiseCommand,
                targetingHarmed
            );

            NotTargetableUntil = PlayerController.CurrentClock.ElapsedMilliseconds + 3000;
            PlayerController.EffectsAssembly.CureIceRocket(true);
            PlayerController.EffectsAssembly.CureSlowMine(true);
            PlayerController.EffectsAssembly.CureSlowRocket(true);

            PlayerController.EntitiesInRangeSafe(x => {
                x.Send(empCommand, noiseCommand, targetingHarmed);

                if (x.MovementAssembly.ActualPosition().DistanceTo(PlayerController.MovementAssembly.ActualPosition()) < 500 && x.EffectsAssembly.Cloaked) {
                    x.EffectsAssembly.UnCloak();
                }

                if (x.Locked != null && x.Locked.ID == PlayerController.ID) {
                    x.Lock(null);
                }
            });
        }

        public void TriggerISH() {
            SpecialItem item = SpecialItem.ISH_01;

            if (!PlayerController.Account.Vault.SpecialItems.TryGetValue(item.ID, out int currentCount)
                || currentCount <= 0) {
                return;
            }
            PlayerController.Account.Vault.SpecialItems[item.ID] = --currentCount;

            PlayerController.Account.Cooldown.SpecialItemCooldown.TryGetValue(item.ID, out DateTime lastTrigger);
            if (lastTrigger.FromNow() < item.Cooldown) {
                return;
            }

            PlayerController.Account.Cooldown.SpecialItemCooldown[item.ID] = DateTime.Now;
            InvincibleUntil = PlayerController.CurrentClock.ElapsedMilliseconds + 3000;

            ICommand ishComamnd = PacketBuilder.SpecialItems.Instashield(PlayerController);
            PlayerController.Send(
                PacketBuilder.Slotbar.ExplosiveItemStatus(item.Name, currentCount, false),
                PacketBuilder.Slotbar.ItemCooldownCommand(item.Name, item.Cooldown.TotalMilliseconds, currentCount > 0),
                ishComamnd
            );
            PlayerController.EntitesInRange(x => x.Send(ishComamnd));
        }

        public override void Refresh() { }
        #endregion

    }
}
