using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies;
using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Emulator.Network.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Emulator.Services;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers {
    public class PlayerController : EntityControllerBase {

        #region {[ PROPERTIES ]}
        public AccountView Account { get; set; }
        public UserClientConfiguration ClientConfiguration { get; set; }
        public GameConnectionHandler Connection { get; set; }

        public PlayerDroneFormationAssembly DroneFormationAssembly { get; set; }
        public PlayerSpecialItemsAssembly SpecialItemsAssembly { get; set; }
        public PlayerTechAssembly PlayerTechAssembly { get; set; }
        public PlayerAbilityAssembly PlayerAbilityAssembly { get; set; }
        public PlayerGroupAssembly PlayerGroupAssembly { get; set; }
        public PlayerItemsAssembly PlayerItemsAssembly { get; set; }
        public PlayerHangarAssembly PlayerHangarAssembly => (PlayerHangarAssembly)HangarAssembly;
        public PlayerEffectsAssembly PlayerEffectsAssembly => (PlayerEffectsAssembly)EffectsAssembly;

        public bool IsInLogoutProcess { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private TickInterval _checkMinesInRange;
        private TickInterval _checkPlayersInRange;
        private TickInterval _checkZonesInRange;
        private TickInterval _updateState;
        private TickInterval _checkConnectionExists;
        private TickInterval _checkLogout;

        private HashSet<int> _renderedMines;
        private HashSet<int> _renderedPlayers;

        private long _lastConnectionExist;
        private bool _disposed;
        #endregion

        #region {[ CONSTRUCTOR & INITIALIZER ]}
        public PlayerController(AccountView account) : base(account.ID, account.Username, account.Faction) {
            Account = account ?? throw _logger.LogError(new ArgumentNullException(nameof(account)));

            if (!string.IsNullOrWhiteSpace(Account.UserClientConfiguration)) {
                ClientConfiguration = JsonConvert.DeserializeObject<UserClientConfiguration>(Account.UserClientConfiguration);
            } else {
                ClientConfiguration = UserClientConfiguration.Instance();
            }

            InitializeAssemblies();

            _renderedMines = new HashSet<int>();
            _renderedPlayers = new HashSet<int>();

            _checkPlayersInRange = new TickInterval(CheckPlayersInRange, 100);
            _checkMinesInRange = new TickInterval(CheckMinesInRange, 200);
            _checkZonesInRange = new TickInterval(CheckZonesInRange, 300);
            _updateState = new TickInterval(UpdateState, 1000 * 60 * 5); // 5 Minuten
            _checkConnectionExists = new TickInterval(CheckConnectionExists, 3000);
            _checkLogout = new TickInterval(CheckLogout, 5000);

            OnTick += TickEvent;
        }

        private void InitializeAssemblies() {
            BoosterAssembly = new BoosterAssembly(this);
            HangarAssembly = new PlayerHangarAssembly(this);
            MovementAssembly = new PlayerMovementAssembly(this);
            DroneFormationAssembly = new PlayerDroneFormationAssembly(this);
            AttackAssembly = new PlayerAttackAssembly(this);
            SpecialItemsAssembly = new PlayerSpecialItemsAssembly(this);
            EffectsAssembly = new PlayerEffectsAssembly(this);
            AttackTraceAssembly = new AttackTraceAssembly(this);
            ZoneAssembly = new ZoneAssembly(this);
            PlayerTechAssembly = new PlayerTechAssembly(this);
            PlayerAbilityAssembly = new PlayerAbilityAssembly(this);
            PlayerGroupAssembly = new PlayerGroupAssembly(this);
            PlayerItemsAssembly = new PlayerItemsAssembly(this);

            BoosterAssembly.Multiply(BoosterType.DAMAGE, 1.6);
            BoosterAssembly.Multiply(BoosterType.DAMAGE, 2);

            BoosterAssembly.Multiply(BoosterType.SHIELD, 1.7);
            BoosterAssembly.Multiply(BoosterType.SPEED, 1.2);
            BoosterAssembly.Multiply(BoosterType.HITPOINTS, 1.2);
            BoosterAssembly.Multiply(BoosterType.HIT_RATE, 0.8);

            InitializeTimer();
        }

        public void Initialize(GameConnectionHandler connection, bool killSession) {
            if (killSession && Connection != null) {
                Connection.Dispose(false);
                Connection = null;
            }

            Connection = connection;
            _lastConnectionExist = -1;

            Refresh(!killSession);
        }
        #endregion

        #region {[ COMMANDS ]}
        public override void Send(ICommand command) {
            if (Connection != null && !Connection.IsDisposed) {
                Connection.Send(command);
            }
        }

        public override void Send(params ICommand[] commands) {
            if (Connection != null && !Connection.IsDisposed) {
                Connection.Send(commands);
            }
        }

        public override void Send(IEnumerable<ICommand> commands) {
            if (Connection != null && !Connection.IsDisposed) {
                Connection.Send(commands);
            }
        }
        #endregion

        #region {[ HANDLER ]}
        public override void Lock(EntityControllerBase entity) {

            if (Locked != null && (entity == null || Locked.ID != entity.ID)) {
                PlayerAbilityAssembly.CheckOrStopSingularity(true);
            }

            base.Lock(entity);
        }

        public override void Die() {
            PlayerEffectsAssembly.CureInfection(true);
            MovementAssembly.Move(MovementAssembly.ActualPosition(), MovementAssembly.ActualPosition());

            Lock(null);
            EntitiesLockedSafe(x => {
                if (x.Locked != null && x.Locked.ID == ID) {
                    x.Lock(null);
                }
            });

            ICommand killCommand = PacketBuilder.KillCommand(this);

            GameManager.Get(AttackTraceAssembly.CurrentMainAttacker, out PlayerController killer);
            Send(killCommand, PacketBuilder.KillScreen.KillScreenCommand(DestructionTypeModule.PLAYER, killer));
            // send kill screen

            EntitesInRange(x => {
                if (x.ID == AttackTraceAssembly.CurrentMainAttacker) { // killer
                                                                       // render rewards etc.
                    x.Send(
                        killCommand,
                        PacketBuilder.Legacy("0|A|STD|You killed " + Account.Username + "!")
                   );

                } else {
                    x.Send(killCommand);
                }
            });

            AttackTraceAssembly.Reset();
            Spacemap?.Remove(this); // remove from spacemap
            TimerStop();
        }

        public async void Refresh(bool wasKilled) {
            if (HangarAssembly.Hitpoints <= 0) {
                Die();
                return;
            }

            HangarAssembly.Refresh();

            Send(
                ClientConfiguration.UserSettings,
                PacketBuilder.UserKeyBindinsCommand(this),
                PacketBuilder.UIMenuBarsCommand(this),
                PacketBuilder.Slotbar.SlotBarsCommand(this)
            );

            Send(
                new class_884(new List<class_503>() { new class_571(false, 1) }),
                PacketBuilder.Legacy("0|A|BKBB|0"),
                PacketBuilder.Legacy("0|A|BKS|0"),
                PacketBuilder.Legacy("0|A|BKB|0"),
                PacketBuilder.Legacy("0|A|BKPR|0"),
                PacketBuilder.Legacy("0|A|BKR|0"),
                PacketBuilder.Legacy("0|A|BKM|0"),
                PacketBuilder.Legacy("0|A|BK|0"),
                PacketBuilder.Legacy("0|A|JV|0"),
                PacketBuilder.Legacy("0|TR")
            );

            Send(
                PacketBuilder.InitializeShipCommand(this),
                PacketBuilder.DroneCommand(this),
                PacketBuilder.ConfigurationCommand(this),
                PacketBuilder.SpeedChangeCommand(this),
                ZoneAssembly.ZoneCommand(),
                PacketBuilder.Legacy("0|8"), // login done
                PacketBuilder.Legacy("0|7|HS")
            );

            await Task.Delay(750);

            Send(EffectsAssembly.EffectsCommand()
                .Concat(PlayerTechAssembly.EffectsCommand())
                .Concat(PlayerAbilityAssembly.EffectsCommand()));

            TimerStart();

            if (wasKilled) {
                EffectsAssembly.MakeInvincible(10000);
            }

            lock (_checkMinesInRange) {
                _renderedMines.Clear();
            }

            lock (_checkPlayersInRange) {
                _renderedPlayers.Clear();
            }

            SpacemapController.For(Account.CurrentHangar.MapID).Remove(this);
            SpacemapController.For(Account.CurrentHangar.MapID).Add(this);

            if (Locked != null) { // just for the visuals
                SendLockVisual(Locked);
            }
        }

        public override void Refresh() {
            Refresh(false);
        }

        public override void EntityAddedToMap(EntityControllerBase entity) {
            switch (entity) {
                case PlayerController playerEntity:
                    Send(
                        PacketBuilder.CreateShipCommand(this, playerEntity),
                        PacketBuilder.DroneCommand(playerEntity),
                        playerEntity.MovementAssembly.MovementCommand()
                    );

                    Send(playerEntity.PlayerTechAssembly.EffectsCommand()
                        .Concat(playerEntity.PlayerAbilityAssembly.EffectsCommand()));
                    break;
                default:
                    Send(
                        PacketBuilder.CreateShipCommand(this, entity),
                        entity.MovementAssembly.MovementCommand());
                    break;
            }

            Send(entity.EffectsAssembly.EffectsCommand());
        }

        public override void EntityRemovedFromMap(int id) {
            if (Locked != null && Locked.ID == id) {
                Lock(null);
            }

            Send(PacketBuilder.ShipRemoveCommand(id));
        }

        public async void DeclareBan(string reason, TimeSpan banTime) {
            try {
                var result = await AccountService.DeclareBan(new AccountDeclareBanView {
                    ID = ID,
                    Reason = reason,
                    Duration = banTime
                });

                if (!result.IsValid) {
                    _logger.LogCritical($"Failed to ban [UID: {ID}]: {result.Message}");
                }
            } catch (Exception e) {
                _logger.LogError(e);

                await Task.Delay(2500).ContinueWith((x) => {
                    _logger.LogInformation($"Retrying banning [UID: {ID}] Reason: {reason}");
                    DeclareBan(reason, banTime);
                });
            }
        }
        #endregion

        #region {[ TIMING ]}
        private void TickEvent(double timeSinceLastTick) {
            _checkMinesInRange.Tick(timeSinceLastTick);
            _checkPlayersInRange.Tick(timeSinceLastTick);
            _checkZonesInRange.Tick(timeSinceLastTick);
            _updateState.Tick(timeSinceLastTick);
            _checkConnectionExists.Tick(timeSinceLastTick);

            if (IsInLogoutProcess) {
                _checkLogout.Tick(timeSinceLastTick);
            }
        }

        private void CheckPlayersInRange() {
            if (Spacemap == null) {
                return;
            }

            HashSet<int> updated = new HashSet<int>();
            lock (_checkPlayersInRange) {
                foreach (var entity in Spacemap.InRange(this)) {
                    if (!_renderedPlayers.Contains(entity.ID)) {
                        EntityAddedToMap(entity);
                    }

                    updated.Add(entity.ID);
                }

                foreach (int id in _renderedPlayers) {
                    if (!updated.Contains(id)) {
                        EntityRemovedFromMap(id);
                    }
                }
            }
            _renderedPlayers = updated;
        }

        private void CheckMinesInRange() {
            if (Spacemap == null) {
                return;
            }

            HashSet<int> updated = new HashSet<int>();
            lock (_checkMinesInRange) {
                foreach (var mine in Spacemap.MinesInRange(this).ToList()) {
                    if (!_renderedMines.Contains(mine.ID)) {
                        Send(mine.Render());
                    }

                    updated.Add(mine.ID);

                    // ??? besserer ort erwünscht
                    mine.Check(this);
                }

                foreach (int id in _renderedMines) {
                    if (!updated.Contains(id)) {
                        Send(new MineRemoveCommand(id.ToString()));
                    }
                }
            }
            _renderedMines = updated;
        }

        private void CheckZonesInRange() {
            if (Spacemap == null) {
                return;
            }

            // bei einer Base bekommt man dann NAZ, wenn man selber 5sekunden nicht angreift
            // bei einem Portal bekommt man dann NAZ, wenn man selber 5sekunden nicht angreift und 3sekunden nicht angegriffen wurde.

            // Portal NAZ Radius: 500 (Durchmesser 1000)
            // Base NAZ Radius: 1793 (Durchmesser: 3586) (Die Türme bestimmen die NAZ)

            Position currentPosition = MovementAssembly.ActualPosition();
            lock (_checkZonesInRange) {

                bool isNaz = false;
                bool canEquip = false;

                if (!Spacemap.MapInfo.IsBattleMap) {

                    foreach (PortalObject portal in Spacemap.MapInfo.Portals) {
                        if (portal.OwnerFaction.ID == Faction.ID
                            && portal.Position.DistanceTo(currentPosition) < 500
                            && AttackAssembly.LastAttack.FromNow(CurrentClock.ElapsedMilliseconds) > 5000
                            && AttackTraceAssembly.LastAttackTime.FromNow(CurrentClock.ElapsedMilliseconds) > 3000) {
                            isNaz = true;
                            break;
                        }
                    }

                }

                foreach (BaseObject @base in Spacemap.MapInfo.Bases) {
                    if (@base.OwnerFaction.ID == Faction.ID
                        && @base.Position.DistanceTo(currentPosition) < 1793
                        && AttackAssembly.LastAttack.FromNow(CurrentClock.ElapsedMilliseconds) > 5000) {
                        isNaz = true;
                        canEquip = true;
                        break;
                    }
                }


                ZoneAssembly.ChangeEquip(canEquip);

                if (isNaz) {
                    ZoneAssembly.ShowDMZ();
                } else {
                    ZoneAssembly.HideDMZ();
                }
            }
        }

        private void CheckConnectionExists() {
            if (Connection == null) {
                if (_lastConnectionExist == -1) {
                    _lastConnectionExist = CurrentClock.ElapsedMilliseconds;
                }

                if (CurrentClock.ElapsedMilliseconds - _lastConnectionExist > 30000) {
                    _lastConnectionExist = -1;
                    Dispose();
                }
            }
        }

        public bool TryRemoveMine(MineObject mine) {
            lock (_checkMinesInRange) {
                return _renderedMines.Remove(mine.ID);
            }
        }
        #endregion

        #region {[ LOGOUT, DISPOSE & DESTRUCTOR ]}
        public void Logout(bool force = false) {
            if (force) {
                Connection = null;
                _lastConnectionExist = CurrentClock.ElapsedMilliseconds;
                return;
            }

            _checkLogout.Reset(Account.IsPremium ? 5000 : 20000);
            IsInLogoutProcess = true;
        }

        public void CancelLogout() {
            if (!IsInLogoutProcess) {
                return;
            }

            IsInLogoutProcess = false;
            Send(PacketBuilder.Legacy("0|t"));
        }

        private void CheckLogout() {
            if (IsInLogoutProcess) {
                Dispose();
            }
        }

        public async void UpdateState() {
            try {
                Account.UserClientConfiguration = JsonConvert.SerializeObject(ClientConfiguration);
                if (!(await AccountService.UpdateAccount(Account)).IsValid) {
                    _logger.LogWarning($"['{Account.Username}'] failed to update state");
                }
                _logger.LogInformation($"['{Account.Username}'] state updated!");
            } catch (Exception e) {
                _logger.LogError(e);
            }
        }

        public override void Dispose() {
            if (_disposed || !(_disposed = true)) {
                return;
            }

            Lock(null);
            Spacemap?.Remove(this);

            GameManager.Remove(ID);
            UpdateState();

            base.Dispose();
            Connection?.Dispose(false);
            _logger.LogInformation($"['{Account.Username}'] unloaded!");
        }
        #endregion

    }
}
