using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EpicOrbit.Client.Services.Abstracts;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Vault;

namespace EpicOrbit.Client.Services {
    public class AccountService : ApiServiceBase {

        public AccountService(ApiClient client) : base(client) { }

        public bool IsAuthenticated() {
            return Api().AccountSessionView != null;
        }

        public bool Login(string username, string password, out string message) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().Create("api/account/login", "post").WithBody(new AccountLoginView {
                Username = username,
                Password = password
            }).Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out HttpStatusCode code) && code == HttpStatusCode.OK) {
                ValidatedView<AccountSessionView> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<AccountSessionView>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    message = validatedView.Message;
                    if (validatedView.IsValid) {
                        Api().AccountSessionView = validatedView.Object;
                    }
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool Register(string username, string password, string email, int faction, out string message) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().Create("api/account/register", "post").WithBody(new AccountRegisterView {
                Username = username,
                Password = password,
                Email = email,
                Faction = faction
            }).Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out HttpStatusCode code) && code == HttpStatusCode.OK) {
                ValidatedView validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveAccountOverview(out AccountOverview accountOverview, out string message) {
            return RetrieveAccountOverview(out accountOverview, out message, out _);
        }

        public bool RetrieveAccountOverview(out AccountOverview accountOverview, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            accountOverview = null;

            Api().CreateAuthenticated("api/account", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<AccountOverview> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<AccountOverview>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    accountOverview = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveAccountPlayerOnline(out AccountPlayersOnline accountPlayersOnline, out string message) {
            return RetrieveAccountPlayerOnline(out accountPlayersOnline, out message, out _);
        }

        public bool RetrieveAccountPlayerOnline(out AccountPlayersOnline accountPlayersOnline, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            accountPlayersOnline = null;

            Api().CreateAuthenticated("api/account/online", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<AccountPlayersOnline> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<AccountPlayersOnline>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    accountPlayersOnline = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveAccountVault(out VaultView vaultView, out string message) {
            return RetrieveAccountVault(out vaultView, out message, out _);
        }

        public bool RetrieveAccountVault(out VaultView vaultView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            vaultView = null;

            Api().CreateAuthenticated("api/account/vault", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<VaultView> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<VaultView>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    vaultView = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

    }
}
