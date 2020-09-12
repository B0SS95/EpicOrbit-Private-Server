using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Shared.Enumerables;
using System;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerItemsAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; protected set; }
        #endregion

        #region {[ FIELDS ]}
        private object _lock;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerItemsAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
            _lock = new object();
        }
        #endregion

        #region {[ HELPERS ]}
        private bool Change(int change, Func<long> getter, Action<long> setter, bool allowNegative = false) {
            lock (_lock) {
                bool result = true;

                if (change < 0 && getter() + change < 0) {
                    result = false;
                    if (!allowNegative) {
                        return result;
                    }
                }

                setter(getter() + change);
                return result;
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public bool ChangeUridium(int change, bool allowNegative = false) {
            return Change(change, () => PlayerController.Account.Uridium, result => {
                PlayerController.Account.Uridium = result;
                PlayerController.Send(PacketBuilder.Rewards.Uridium(change, result));
            }, allowNegative);
        }

        public bool ChangeCredits(int change, bool allowNegative = false) {
            return Change(change, () => PlayerController.Account.Credits, result => {
                PlayerController.Account.Credits = result;
                PlayerController.Send(PacketBuilder.Rewards.Credits(change, result));
            }, allowNegative);
        }

        public bool ChangeHonor(int change, bool allowNegative = false) {
            if (change > 0) {
                if (Controller.Spacemap?.MapInfo.IsBattleMap ?? false) {
                    change *= 2;
                }
                change += (int)(change * Controller.BoosterAssembly.Get(BoosterType.HONOR));
            }

            return Change(change, () => PlayerController.Account.Honor, result => {
                PlayerController.Account.Honor = result;
                PlayerController.Send(PacketBuilder.Rewards.Honor(change, result));
            }, allowNegative);
        }

        public bool ChangeExperience(int change, bool allowNegative = false, bool isNpc = false) {
            if (change > 0) {
                if (isNpc) {
                    change += (int)(change * Controller.BoosterAssembly.Get(BoosterType.EXPERIENCE_PVE));
                }

                if (Controller.Spacemap?.MapInfo.IsBattleMap ?? false) {
                    change *= 2;
                }

                change += (int)(change * Controller.BoosterAssembly.Get(BoosterType.EXPERIENCE));
            }

            int level = PlayerController.Account.Level;
            return Change(change, () => PlayerController.Account.Experience, result => {
                PlayerController.Account.Experience = result;
                PlayerController.Send(PacketBuilder.Rewards.Experience(change, result, level));
            }, allowNegative);
        }

        public override void Refresh() { }
        #endregion

    }
}
