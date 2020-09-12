using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;

namespace EpicOrbit.Client.Services {
    public class ApiClient {

        #region {[ STATIC FUNCTIONS ]}
        public static HttpWebRequest CreateRequest(string url, string method) {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{ClientContext.Host}/{url}");
            httpWebRequest.Method = method;

            return httpWebRequest;
        }
        #endregion

        #region {[ PROPERTIES ]}
        public AccountSessionView AccountSessionView { get; set; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ApiClient() { }
        public ApiClient(AccountSessionView accountSessionView) {
            AccountSessionView = accountSessionView;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public HttpWebRequest Create(string url, string method) {
            return CreateRequest(url, method);
        }

        public HttpWebRequest CreateAuthenticated(string url, string method) {
            return Create(url, method).WithAuthentification(this);
        }
        #endregion

    }
}
