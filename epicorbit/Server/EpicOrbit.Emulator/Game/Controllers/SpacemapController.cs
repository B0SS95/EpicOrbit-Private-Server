using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Emulator.Extensions;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers {
    public class SpacemapController {

        #region {[ STATIC ]}
        private static Dictionary<int, SpacemapController> _controllers = new Dictionary<int, SpacemapController>();
        public static SpacemapController For(int map) {
            lock (_controllers) {
                if (!_controllers.TryGetValue(map, out SpacemapController controller)) {
                    try {
                        controller = new SpacemapController(map);
                        _controllers.Add(map, controller);

                        // Fake npc zum testen
                        new NpcController(111111, "Ehrenhaftes NPC 111111", Faction.NONE);

                        GameContext.Logger.LogInformation($"Map {controller.MapInfo.Name} created!");
                    } catch (Exception e) {
                        GameContext.Logger.LogError(e);
                    }
                }
                return controller;
            }
        }
        #endregion

        #region {[ PROPERTIES ]}
        public int ID => MapInfo.ID;
        public Map MapInfo { get; }
        public Dictionary<int, EntityControllerBase> Players => _entities;
        public Dictionary<int, MineObject> Mines => _mines;
        #endregion

        #region {[ FIELDS ]}
        private int _enemyCounter;
        private Dictionary<int, EntityControllerBase> _entities;
        private Dictionary<int, MineObject> _mines;

        private ReaderWriterLockSlim _lock;
        private ReaderWriterLockSlim _mineLock;
        private List<ICommand> _mapDataCommands;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public SpacemapController(int map) {
            MapInfo = ItemsExtension<Map>.Lookup(map);

            _entities = new Dictionary<int, EntityControllerBase>();
            _mines = new Dictionary<int, MineObject>();

            _lock = new ReaderWriterLockSlim();
            _mineLock = new ReaderWriterLockSlim();
        }
        #endregion

        #region {[ MANAGEMENT ]}
        public void Add(EntityControllerBase controller) {
            using (_lock.ObtainUpgradeableReadLock()) {

                if (controller.Spacemap != null && controller.Spacemap.ID != ID) { // avoid deadlock
                    controller.Spacemap.Remove(controller);
                }

                bool updateEnemyCounter = false;
                if (!_entities.ContainsKey(controller.ID)) {
                    using (_lock.ObtainWriteLock()) {
                        if (MapInfo.IsStarter && controller.Faction.ID > 0 && controller.Faction.ID != MapInfo.OwnerFaction.ID) {
                            _enemyCounter++;
                            updateEnemyCounter = true;
                        }
                        _entities.Add(controller.ID, controller);
                    }
                }

                if (MapInfo.IsStarter) {
                    ICommand warnBoxCommand = PacketBuilder.MapWarnCountCommand(_enemyCounter);
                    if (controller.Faction.ID == MapInfo.OwnerFaction.ID) {
                        controller.Send(warnBoxCommand);
                    }

                    foreach (var other in _entities.Values) { // always two way

                        if (updateEnemyCounter && other.Faction.ID == MapInfo.OwnerFaction.ID) {
                            other.Send(warnBoxCommand);
                        }

                    }
                }

                if (_mapDataCommands == null) {
                    _mapDataCommands = new List<ICommand>();
                    foreach (PortalObject portal in MapInfo.Portals) {
                        _mapDataCommands.Add(PacketBuilder.CreatePortalCommand(portal));
                    }

                    foreach (BaseObject @base in MapInfo.Bases) {
                        foreach (AssetObject asset in @base.From()) {
                            _mapDataCommands.Add(asset.Render());
                        }
                    }
                }

                controller.Send(_mapDataCommands);

            }

            controller.Spacemap = this;
            controller.HangarAssembly.Map = MapInfo;
        }

        public void Remove(EntityControllerBase controller) {
            bool updateEnemyCounter = false;
            using (_lock.ObtainUpgradeableReadLock()) {
                if (_entities.ContainsKey(controller.ID)) {
                    using (_lock.ObtainWriteLock()) {
                        if (MapInfo.IsStarter && controller.Faction.ID > 0
                            && controller.Faction.ID != MapInfo.OwnerFaction.ID) {
                            _enemyCounter--;
                            updateEnemyCounter = true;
                        }
                        _entities.Remove(controller.ID);
                    }
                }
            }

            if (controller is PlayerController playerController
                && playerController.PlayerAbilityAssembly.WeakenShieldsActive) {
                playerController.PlayerAbilityAssembly.CheckOrStopWeakenShields(true);
            }

            ICommand warnBoxCommand = PacketBuilder.MapWarnCountCommand(_enemyCounter);
            foreach (var other in EntitiesOnMap(controller).ToList()) {
                if (MapInfo.IsStarter && updateEnemyCounter && other.Faction.ID == MapInfo.OwnerFaction.ID) {
                    other.Send(warnBoxCommand);
                }

                if (other is PlayerController playerController2
                    && playerController2.PlayerAbilityAssembly.WeakenShieldsActive
                    && playerController2.PlayerAbilityAssembly.WeakenShieldsVictim.ID == controller.ID) {
                    playerController2.PlayerAbilityAssembly.CheckOrStopWeakenShields(true);
                }
            }

        }

        public void Add(MineObject mine) {
            using (_mineLock.ObtainUpgradeableReadLock()) {

                if (!_mines.ContainsKey(mine.ID)) {
                    using (_mineLock.ObtainWriteLock()) {
                        _mines.Add(mine.ID, mine);
                    }
                }

            }
        }

        public void Remove(MineObject mine) {
            using (_mineLock.ObtainUpgradeableReadLock()) {

                if (_mines.ContainsKey(mine.ID)) {
                    using (_mineLock.ObtainWriteLock()) {
                        _mines.Remove(mine.ID);
                    }
                }

            }
        }
        #endregion

        #region {[ HELPER ]}
        private bool IsInRange(EntityControllerBase controller, EntityControllerBase entity, int range) {
            return entity.ID != controller.ID &&
                       (entity.Locked != null && entity.Locked.ID == controller.ID || controller.Locked != null && controller.Locked.ID == entity.ID ||
                       entity.MovementAssembly.ActualPosition().DistanceTo(controller.MovementAssembly.ActualPosition()) <= range);
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public IEnumerable<EntityControllerBase> EntitiesOnMap(EntityControllerBase controller) {
            using (_lock.ObtainReadLock()) {
                foreach (var entity in _entities) {
                    if (entity.Value.ID != controller.ID) {
                        yield return entity.Value;
                    }
                }
            }
        }

        public IEnumerable<MineObject> MinesInRange(EntityControllerBase controller) {
            const int range = 850;

            using (_mineLock.ObtainReadLock()) {
                foreach (var mine in _mines) {
                    if (mine.Value.Position.DistanceTo(controller.MovementAssembly.ActualPosition()) <= range) {
                        yield return mine.Value;
                    }
                }
            }
        }

        public IEnumerable<EntityControllerBase> InRange(Position position, int range = -1) {
            if (range < MapInfo.ViewRange) {
                range = MapInfo.ViewRange;
            }

            using (_lock.ObtainReadLock()) {
                foreach (var entity in _entities) {
                    if (entity.Value.MovementAssembly.ActualPosition().DistanceTo(position) <= range) {
                        yield return entity.Value;
                    }
                }
            }
        }

        // In range or have me in lock or i have them in lock
        public IEnumerable<EntityControllerBase> InRange(EntityControllerBase controller, int range = -1) {
            if (range < MapInfo.ViewRange) {
                range = MapInfo.ViewRange;
            }

            using (_lock.ObtainReadLock()) {
                foreach (var entity in _entities) {
                    if (IsInRange(controller, entity.Value, range)) {
                        yield return entity.Value;
                    } else if (entity.Value.ID != controller.ID &&
                         (entity.Value.Locked != null && entity.Value.Locked.ID == controller.ID || controller.Locked != null && controller.Locked.ID == entity.Value.ID ||
                         entity.Value.MovementAssembly.ActualPosition().DistanceTo(controller.MovementAssembly.ActualPosition()) <= range)) {
                        yield return entity.Value;
                    }
                }
            }
        }

        // Have me in Lock
        public IEnumerable<EntityControllerBase> Locked(EntityControllerBase controller) {
            using (_lock.ObtainReadLock()) {
                foreach (var entity in _entities) {
                    if (entity.Value.ID != controller.ID &&
                        entity.Value.Locked != null && entity.Value.Locked.ID == controller.ID) {
                        yield return entity.Value;
                    }
                }
            }
        }
        #endregion

    }
}
