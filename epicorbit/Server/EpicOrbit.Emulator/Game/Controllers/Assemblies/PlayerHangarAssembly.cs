using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerHangarAssembly : HangarAssembly {

        #region {[ CONSTANTS ]}
        private const long CONFIGURATION_COOLDOWN = 5000;
        #endregion

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; protected set; }

        public int DamagePVP => (int)(Damage * Controller.BoosterAssembly.Get(BoosterType.DAMAGE_LASER_PVP));
        public int DamagePVE => (int)(Damage * Controller.BoosterAssembly.Get(BoosterType.DAMAGE_LASER_PVE));

        public override Ship Ship {
            get {
                if (_ship == null) {
                    _ship = PlayerController.Account.CurrentHangar.ShipID.FromShips();
                }
                return _ship;
            }

            set {
                _ship = value;
                PlayerController.Account.CurrentHangar.ShipID = _ship.ID;
            }
        }
        private Ship _ship;

        public override Map Map {
            get {
                if (_map == null) {
                    _map = ItemsExtension<Map>.Lookup(PlayerController.Account.CurrentHangar.MapID);
                }
                return _map;
            }

            set {
                _map = value;
                PlayerController.Account.CurrentHangar.MapID = _map.ID;
            }
        }
        private Map _map;

        public override Position Position {
            get => PlayerController.Account.CurrentHangar.Position;
            set => PlayerController.Account.CurrentHangar.Position = value;
        }

        protected override int _hitpoints {
            get => PlayerController.Account.CurrentHangar.Hitpoints;
            set => PlayerController.Account.CurrentHangar.Hitpoints = value;
        }
        protected override int _maxHitpoints => PlayerController.Account.CurrentHangar.MaxHitpoints;

        protected override int _shield {
            get => PlayerController.Account.CurrentHangar.Shield;
            set => PlayerController.Account.CurrentHangar.Shield = value;
        }
        protected override int _maxShield => PlayerController.Account.CurrentHangar.MaxShield;

        protected override int _damage => PlayerController.Account.CurrentHangar.Damage;
        protected override int _speed => PlayerController.Account.CurrentHangar.Speed;
        #endregion

        #region {[ FIELDS ]}
        private long _lastConfigurationChangeTime;
        #endregion

        public PlayerHangarAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;

            PlayerController.BoosterAssembly.Multiply(BoosterType.SHIELD_REGNERATION,
                Math.Max(0.0000001, PlayerController.Account.CurrentHangar.CurrentConfiguration.Regeneration));

            PlayerController.BoosterAssembly.Multiply(BoosterType.HITPOINTS_REGENERATION, 0.03);

            PlayerController.BoosterAssembly.Multiply(BoosterType.SHIELD_ABSORBATION,
                Math.Max(0.0000001, PlayerController.Account.CurrentHangar.CurrentConfiguration.Absorption));
        }

        public void ChangeConfiguration() {
            long currentTime = PlayerController.CurrentClock.ElapsedMilliseconds;
            if (_lastConfigurationChangeTime.FromNow(currentTime) < CONFIGURATION_COOLDOWN) {
                PlayerController.Send(PacketBuilder.Messages.ConfigurationChangeFailed());
                return;
            }

            _lastConfigurationChangeTime = currentTime;

            double absorption = PlayerController.Account.CurrentHangar.CurrentConfiguration.Absorption;
            double regeneration = PlayerController.Account.CurrentHangar.CurrentConfiguration.Regeneration;

            PlayerController.Account.CurrentHangar.Configuration = !PlayerController.Account.CurrentHangar.Configuration;

            PlayerController.BoosterAssembly.Set(BoosterType.SHIELD_ABSORBATION,
                PlayerController.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION) / Math.Max(0.0000001, absorption) *
                Math.Max(0.0000001, PlayerController.Account.CurrentHangar.CurrentConfiguration.Absorption));

            PlayerController.BoosterAssembly.Set(BoosterType.SHIELD_REGNERATION,
                 PlayerController.BoosterAssembly.Get(BoosterType.SHIELD_REGNERATION) / Math.Max(0.0000001, regeneration) *
                 Math.Max(0.0000001, PlayerController.Account.CurrentHangar.CurrentConfiguration.Regeneration));

            Hitpoints = Hitpoints;

            ICommand droneCommand = PacketBuilder.DroneCommand(PlayerController);
            PlayerController.Send(
                PacketBuilder.ConfigurationCommand(PlayerController),
                PacketBuilder.HitpointsChangeCommand(PlayerController),
                PacketBuilder.ShieldChangeCommand(PlayerController),
                PacketBuilder.SpeedChangeCommand(PlayerController),
                droneCommand
            );

            PlayerController.EntitesInRange(entity => {
                entity.Send(droneCommand);
                if (entity.Locked != null && entity.Locked.ID == PlayerController.ID) {
                    entity.Send(PacketBuilder.TargetHealthBaseChangedCommand(Controller));
                }
            });

            PlayerController.MovementAssembly.RefreshMovement();
        }

        public override void Refresh() {
            base.Refresh();

            _ship = null;
            _map = null;
        }

    }
}
