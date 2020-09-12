using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Network.Handlers;
using System.Collections.Generic;

namespace EpicOrbit.Emulator.Game {
    public static class GameManager {

        #region {[ STATIC FIELDS ]}
        private static object _lock = new object();
        #endregion

        #region {[ STATIC PROPERTIES ]}
        public static Dictionary<int, PlayerController> Players { get; } = new Dictionary<int, PlayerController>();
        #endregion

        #region {[ FUNCTIONS ]}
        public static void Attach(int id, GameConnectionHandler client, AccountView account, bool killSession = true) {
            PlayerController controller = null;

            lock (_lock) {
                if (!Players.TryGetValue(id, out controller)) {
                    Players[id] = controller = new PlayerController(account);
                }

                client.Controller = controller;
            }

            if (controller != null) {
                controller.Initialize(client, killSession);
            }
        }

        public static bool Get(int clientId, out PlayerController controller) {
            lock (_lock) {
                return Players.TryGetValue(clientId, out controller);
            }
        }

        public static bool Remove(int clientId) {
            lock (_lock) {
                return Players.Remove(clientId);
            }
        }
        #endregion

    }
}
