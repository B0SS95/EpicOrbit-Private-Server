using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Emulator.Chat.Controllers;
using EpicOrbit.Emulator.Network.Handlers;
using System.Collections.Generic;

namespace EpicOrbit.Emulator.Chat {
    public static class ChatManager {


        #region {[ STATIC FIELDS ]}
        private static object _lock = new object();
        #endregion

        #region {[ STATIC PROPERTIES ]}
        public static Dictionary<string, ChatUserController> Players { get; } = new Dictionary<string, ChatUserController>();
        #endregion

        #region {[ FUNCTIONS ]}
        public static void Attach(string id, ChatConnectionHandler client, AccountChatView account) {
            ChatUserController controller = null;

            lock (_lock) {
                if (!Players.TryGetValue(id, out controller)) {
                    Players[id] = controller = new ChatUserController(account);
                }

                client.Controller = controller;
            }

            if (controller != null) {
                controller.Initialize(client);
            }
        }

        public static bool Get(string id, out ChatUserController controller) {
            lock (_lock) {
                return Players.TryGetValue(id, out controller);
            }
        }
        #endregion

    }
}
