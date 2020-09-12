using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class MineSelectionHandler : ItemSelectionHandlerBase<Mine> {

        #region {[ INSTANCE ]}
        public static MineSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new MineSelectionHandler();
                }
                return _instance;
            }
        }
        private static MineSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out Mine mine)) {
                if (playerController.Account.Cooldown.LastMine.FromNow() > Mine.Cooldown) {

                    if (!playerController.Account.Vault.Mines.TryGetValue(mine.ID, out int currentCount)
                        || currentCount <= 0) {
                        return;
                    }

                    #region {[ CHECKING ]}
                    // Man kann keine Minen an Basen, an Gates oder während man selber in einer NAZ ist, legen.
                    // D.h. Die neue Map, muss einfach nur die NAZ bei den spielern setzen (nur code, nicht ui)
                    // NPCs müssen lernen zwischen NAZ und schein-NAZ zu unterscheiden.
                    // schein-NAZ = kein pvp

                    if (playerController.ZoneAssembly.IsInDMZ) {
                        return;
                    }

                    Position position = playerController.MovementAssembly.ActualPosition();
                    foreach (PortalObject portal in playerController.Spacemap.MapInfo.Portals) {
                        if (portal.Position.DistanceTo(position) < 500) {
                            return;
                        }
                    }

                    foreach (BaseObject @base in playerController.Spacemap.MapInfo.Bases) {
                        if (@base.Position.DistanceTo(position) < 1793) {
                            return;
                        }
                    }
                    #endregion

                    playerController.Account.Vault.Mines[mine.ID] = --currentCount;


                    playerController.Account.Cooldown.LastMine = DateTime.Now;

                    TimeSpan timeUntilArmed = TimeSpan.FromSeconds(2);
                    if (mine.ID == Mine.SLM_01.ID) {
                        timeUntilArmed = TimeSpan.FromSeconds(1);
                    }

                    playerController.Spacemap.Add(new MineObject(playerController,
                        playerController.Spacemap, playerController.MovementAssembly.ActualPosition(),
                        mine, true, playerController.BoosterAssembly.Get(BoosterType.RADIUS_MINE),
                        true, playerController.BoosterAssembly.Get(BoosterType.DAMAGE_MINE),
                        timeUntilArmed));

                    ICommand[] commands = new ICommand[Items.Count + 1];
                    commands[0] = PacketBuilder.Slotbar.ExplosiveItemStatus(mine.Name, currentCount, false);
                    for (int i = 0; i < Items.Count; i++) {
                        playerController.Account.Vault.Mines.TryGetValue(Items[i].ID, out currentCount);
                        commands[i + 1] = PacketBuilder.Slotbar.ItemCooldownCommand(_items[i], Mine.Cooldown.TotalMilliseconds, currentCount > 0);
                    }

                    playerController.Send(commands);
                }
            }
        }
        #endregion

    }

}
