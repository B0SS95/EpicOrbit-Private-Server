using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Emulator.Network.Handlers;
using System;

namespace EpicOrbit.Emulator.Chat.Controllers {
    public class ChatUserController : IDisposable {

        #region {[ PROPERTIES ]}
        public AccountChatView Account { get; set; }
        public ChatConnectionHandler Connection { get; set; }
        public string GlobalUsername { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private DateTime _logoutDate;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ChatUserController(AccountChatView accountChatView) {
            _logger = GameContext.Logger;
            Account = accountChatView ?? throw _logger.LogError(new ArgumentNullException(nameof(accountChatView)));
            GlobalUsername = $"69_{Account.Username}";
        }

        public void Initialize(ChatConnectionHandler connection) {
            if (Connection != null) {
                Connection.Dispose(false);
                Connection = null;
            }

            Connection = connection;
            Refresh();
        }
        #endregion

        #region {[ HANDLER ]}
        public void Refresh() { // on init



        }
        #endregion

        #region {[ DISPOSEABLE ]}
        public void Logout() {

        }

        public void Dispose() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
