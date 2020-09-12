using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Objects.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Linq;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Emulator.Game.Objects {
    public class MineObject : AddressableObjectBase {

        // detonate Radius = 150
        // explosion Radius = 300

        #region {[ PROPERTIES ]}
        public string Hash => ID.ToString();

        public EntityControllerBase Owner { get; }
        public SpacemapController Spacemap { get; }
        public Position Position { get; }

        public Mine Item { get; set; }
        public double RadiusBoost { get; }
        public double DamageBoost { get; }

        public bool RadiusSkilled { get; }
        public bool DamageSkilled { get; }

        public bool Activated => DateTime.Now.Subtract(_activateableTime) > _armTime;
        #endregion

        #region {[ FIELDS ]}
        private DateTime _activateableTime;
        private TimeSpan _armTime;

        private ICommand _renderCommand;
        private ICommand _removeCommand;
        private ICommand _explodeCommand;

        private bool _detonated;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public MineObject(EntityControllerBase initiator, SpacemapController spacemap, Position position, Mine mine, bool radiusSkilled, double radiusBoost, bool damageSkilled, double damageBoost, TimeSpan readyIn) {
            Owner = initiator;
            Spacemap = spacemap;

            Position = position;
            Item = mine;

            RadiusSkilled = radiusSkilled;
            RadiusBoost = radiusBoost;

            DamageSkilled = damageSkilled;
            DamageBoost = damageBoost;

            _armTime = readyIn;
            _activateableTime = DateTime.Now.Add(_armTime);
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public ICommand Render() {
            if (_renderCommand == null) {
                _renderCommand = new MineCreateCommand(Hash, RadiusSkilled, DamageSkilled, Item.ID, Position.Y, Position.X);
            }
            return _renderCommand;
        }

        public ICommand Remove() {
            if (_removeCommand == null) {
                _removeCommand = new MineRemoveCommand(Hash);
            }
            return _removeCommand;
        }

        public ICommand Explode() {
            if (_explodeCommand == null) {
                _explodeCommand = PacketBuilder.Legacy($"0|n|MIN|{Hash}");
            }
            return _explodeCommand;
        }

        public void Check(PlayerController entity) {
            if (Activated && !_detonated
                && entity.MovementAssembly.ActualPosition().DistanceTo(Position) <= 200 * RadiusBoost) {
                Handle();
            }
        }

        public void Handle() {
            if (Activated && !_detonated) {
                _detonated = true;

                foreach (var entity in Spacemap.InRange(Position, 1000).ToList()) {
                    if (!(entity is PlayerController playerController)) {
                        continue;
                    }

                    if (playerController.TryRemoveMine(this)) {
                        playerController.Send(Explode());
                    } else {
                        playerController.Send(Render(), Explode());
                    }

                    if (playerController.MovementAssembly.ActualPosition().DistanceTo(Position) <= 300 * RadiusBoost) {
                        if (!playerController.EffectsAssembly.HasProtection && !playerController.SpecialItemsAssembly.IsInvicible) {

                            if (Item.ID == Mine.ACM_01.ID) {

                                double damage = playerController.HangarAssembly.Hitpoints * 0.2 * DamageBoost;
                                int shieldDamage = Math.Abs(playerController.HangarAssembly.ChangeShield(-(int)(damage * playerController.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION)), false));
                                int hitpointsDamage = Math.Abs(playerController.HangarAssembly.ChangeHitpoints(-(int)(damage - shieldDamage), false));

                                playerController.AttackTraceAssembly.LogAttack(Owner, shieldDamage, hitpointsDamage, false);
                                playerController.HangarAssembly.CheckDeath();

                                ICommand damageCommand = PacketBuilder.AttackCommand(Owner, playerController, AttackTypeModule.MINE, shieldDamage + hitpointsDamage);
                                playerController.Send(damageCommand);
                                playerController.EntitiesLocked(x => x.Send(damageCommand));

                            } else if (Item.ID == Mine.DDM_01.ID) {

                                int damage = Math.Abs(playerController.HangarAssembly.ChangeHitpoints(-(int)(playerController.HangarAssembly.MaxHitpoints * 0.2 * DamageBoost), false));
                                playerController.AttackTraceAssembly.LogAttack(Owner, 0, damage, false);
                                playerController.HangarAssembly.CheckDeath();

                                ICommand damageCommand = PacketBuilder.AttackCommand(Owner, playerController, AttackTypeModule.MINE, damage);
                                playerController.Send(damageCommand);
                                playerController.EntitiesLocked(x => x.Send(damageCommand));

                            } else if (Item.ID == Mine.IM_01.ID) {

                                playerController.PlayerEffectsAssembly.Infect(15 * 60000);

                                double damage = playerController.HangarAssembly.Hitpoints * 0.2 * DamageBoost;
                                int shieldDamage = Math.Abs(playerController.HangarAssembly.ChangeShield(-(int)(damage * playerController.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION)), false));
                                int hitpointsDamage = Math.Abs(playerController.HangarAssembly.ChangeHitpoints(-(int)(damage - shieldDamage), false));

                                playerController.AttackTraceAssembly.LogAttack(Owner, shieldDamage, hitpointsDamage, false);
                                playerController.HangarAssembly.CheckDeath();

                                ICommand damageCommand = PacketBuilder.AttackCommand(Owner, playerController, AttackTypeModule.MINE, shieldDamage + hitpointsDamage);
                                playerController.Send(damageCommand);
                                playerController.EntitiesLocked(x => x.Send(damageCommand));

                            } else if (Item.ID == Mine.SABM_01.ID) {

                                int damage = Math.Abs(playerController.HangarAssembly.ChangeShield(-(int)(playerController.HangarAssembly.Shield * 0.5 * DamageBoost), false));
                                playerController.AttackTraceAssembly.LogAttack(Owner, damage, 0, false);

                                ICommand damageCommand = PacketBuilder.AttackCommand(Owner, playerController, AttackTypeModule.MINE, damage);
                                playerController.Send(damageCommand);
                                playerController.EntitiesLocked(x => x.Send(damageCommand));

                            } else if (Item.ID == Mine.SLM_01.ID) {

                                playerController.EffectsAssembly.SlowMine(3000);

                            }

                        }

                        if (Item.ID == Mine.EMPM_01.ID) {
                            playerController.EffectsAssembly.UnCloak();
                        }

                    }
                }

                Spacemap.Remove(this);
            }
        }
        #endregion

    }
}
