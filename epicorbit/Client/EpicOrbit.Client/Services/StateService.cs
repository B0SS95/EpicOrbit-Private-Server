using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Client.Pages.Public;
using EpicOrbit.Client.Services.Implementations;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Vault;

namespace EpicOrbit.Client.Services {
    public class StateService : IDisposable {

        #region {[ EVENTS ]}
        public event Action<AccountOverview> OnAccountOverviewRefreshed;
        public event Action<AccountPlayersOnline> OnAccountPlayersOnlineRefreshed;
        #endregion

        #region {[ FIELDS ]}
        private NotificationService _notificationService;
        private ComponentService _componentService;
        private AccountService _accountService;
        private Timer _timerAccount;
        #endregion

        #region {[ PROPERTIES ]}
        public CachedItem<AccountOverview> AccountOverview { get; }
        public CachedItem<AccountPlayersOnline> AccountPlayersOnline { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public StateService(NotificationService notificationService, ComponentService componentService, AccountService accountService) {
            _notificationService = notificationService;
            _componentService = componentService;
            _accountService = accountService;

            AccountOverview = new CachedItem<AccountOverview>(RetrieveAccountOverview, TimeSpan.FromSeconds(10));
            AccountPlayersOnline = new CachedItem<AccountPlayersOnline>(_accountService.RetrieveAccountPlayerOnline, TimeSpan.FromSeconds(10));

            RefreshLoop();
        }
        #endregion

        #region {[ AUTO REFRESH ]}
        public async void RefreshLoop() {
            _timerAccount?.Dispose();
            _timerAccount = new Timer((x) => {
                RefreshAccountOverview();
                RefreshAccountPlayersOnline();
            }, null, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(30));
        }

        private void RefreshAccountOverview() {
            HttpStatusCode code = HttpStatusCode.ServiceUnavailable;
            if (OnAccountOverviewRefreshed != null
                && AccountOverview.Retrieve(out AccountOverview accountOverview, out _, out code)) {
                OnAccountOverviewRefreshed(accountOverview);
            }

            if (code == HttpStatusCode.Unauthorized) {
                _notificationService.ShowWarning("Session expired!");
                _componentService.Show(new Login());
            }
        }

        private void RefreshAccountPlayersOnline() {
            if (OnAccountPlayersOnlineRefreshed != null
                && AccountPlayersOnline.Retrieve(out AccountPlayersOnline accountPlayersOnline, out _, out _)) {
                OnAccountPlayersOnlineRefreshed(accountPlayersOnline);
            }
        }
        #endregion

        private bool RetrieveAccountOverview(out AccountOverview accountOverview, out string message, out HttpStatusCode code) {
            accountOverview = null;
            message = ErrorCode.INVALID_SESSION;
            code = HttpStatusCode.ServiceUnavailable;

            if (!_accountService.IsAuthenticated()
                || !_accountService.RetrieveAccountOverview(out accountOverview, out message, out code)) {
                return false;
            }

            return true;
        }

        #region {[ IDISPOSABLE ]}
        public void Dispose() {
            _timerAccount?.Dispose();
        }
        #endregion

    }
}
