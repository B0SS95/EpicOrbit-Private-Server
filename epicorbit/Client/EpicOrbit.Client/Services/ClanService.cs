using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using EpicOrbit.Client.Services.Abstracts;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Clan;
using EpicOrbit.Shared.ViewModels.Vault;

namespace EpicOrbit.Client.Services {
    public class ClanService : ApiServiceBase {

        public ClanService(ApiClient client) : base(client) { }

        public bool RetrieveClans(string query, int offset, int count, out EnumerableResultView<ClanView> clanView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            clanView = null;

            Api().CreateAuthenticated($"api/clan?query={HttpUtility.UrlEncode(query)}&offset={offset}&count={count}", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<EnumerableResultView<ClanView>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<EnumerableResultView<ClanView>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    clanView = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveClan(out ClanView clanView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            clanView = null;

            Api().CreateAuthenticated("api/clan/current", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<ClanView> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<ClanView>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    clanView = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RevokeJoinRequest(ClanView clanView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/request/{clanView.ID}", "delete")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool CreateJoinRequest(ClanJoinView clanJoinView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;


            Api().CreateAuthenticated("api/clan/request", "post")
                .WithBody(clanJoinView)
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool CreateClan(ClanCreateView clanCreateView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated("api/clan", "post")
                .WithBody(clanCreateView)
               .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool RetrieveMembers(out List<AccountClanView> accountClanViews, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            accountClanViews = null;

            Api().CreateAuthenticated("api/clan/current/members", "get")
               .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<List<AccountClanView>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<List<AccountClanView>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    accountClanViews = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;

        }

        public bool RejectRequest(int accountId, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/current/members/pending/{accountId}", "delete")
            .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool AcceptRequest(int accountId, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/current/members/pending/{accountId}", "put")
            .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool AssignRole(int accountId, int role, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/current/members/role/{accountId}/{role}", "put")
            .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool Leave(out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/leave", "put")
           .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool Edit(ClanCreateView clanUpdateView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/clan/current", "post")
                .WithBody(clanUpdateView)
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
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

        public bool RetrievePending(out List<AccountClanView> accountClanViews, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            accountClanViews = null;

            Api().CreateAuthenticated("api/clan/current/members/pending", "get")
             .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<List<AccountClanView>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<List<AccountClanView>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    accountClanViews = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveSelf(out AccountClanView accountClanView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            accountClanView = null;

            Api().CreateAuthenticated("api/clan/current/members/self", "get")
             .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<AccountClanView> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<AccountClanView>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    accountClanView = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveDiplomacies(out List<ClanDiplomacyView> clanDiplomacies, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            clanDiplomacies = null;

            Api().CreateAuthenticated("api/clan/current/diplomacies", "get")
             .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<List<ClanDiplomacyView>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<List<ClanDiplomacyView>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    clanDiplomacies = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrievePendingDiplomacies(out List<ClanDiplomacyView> clanDiplomacies, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            clanDiplomacies = null;

            Api().CreateAuthenticated("api/clan/current/diplomacies/pending", "get")
             .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<List<ClanDiplomacyView>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<List<ClanDiplomacyView>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    clanDiplomacies = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

    }
}
