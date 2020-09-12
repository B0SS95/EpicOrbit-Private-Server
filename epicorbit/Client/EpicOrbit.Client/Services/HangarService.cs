using EpicOrbit.Client.Services.Abstracts;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Hangar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Services {
    public class HangarService : ApiServiceBase {

        public HangarService(ApiClient client) : base(client) { }

        public bool RetrieveHangarOverview(out List<HangarOverview> hangarOverviews, out string message) {
            return RetrieveHangarOverview(out hangarOverviews, out message, out _);
        }

        public bool RetrieveHangarOverview(out List<HangarOverview> hangarOverviews, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            hangarOverviews = null;

            Api().CreateAuthenticated("api/hangar", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<List<HangarOverview>> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<List<HangarOverview>>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    hangarOverviews = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool RetrieveHangarDetailView(int hangarId, out HangarDetailView hangarDetailView, out string message) {
            return RetrieveHangarDetailView(hangarId, out hangarDetailView, out message, out _);
        }

        public bool RetrieveHangarDetailView(int hangarId, out HangarDetailView hangarDetailView, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;
            hangarDetailView = null;

            Api().CreateAuthenticated($"api/hangar/{hangarId}", "get")
                .Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out code) && code == HttpStatusCode.OK) {
                ValidatedView<HangarDetailView> validatedView = response.GetReponseString()
                    .DeserializeJsonSafe<ValidatedView<HangarDetailView>>();
                if (validatedView == null) {
                    message = ErrorCode.ERROR_WHILE_READING_RESULT;
                } else {
                    hangarDetailView = validatedView.Object;
                    message = validatedView.Message;
                    return validatedView.IsValid;
                }
            }

            return false;
        }

        public bool ActivateHangar(int hangarId, out string message) {
            return ActivateHangar(hangarId, out message, out _);
        }

        public bool ActivateHangar(int hangarId, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/hangar/activate/{hangarId}", "get")
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


        public bool SaveHangar(HangarDetailView hangar, out string message) {
            return SaveHangar(hangar, out message, out _);
        }

        public bool SaveHangar(HangarDetailView hangar, out string message, out HttpStatusCode code) {
            message = ErrorCode.CONNECTION_FAILED;

            Api().CreateAuthenticated($"api/hangar", "post")
                .WithBody(hangar)
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

    }
}
