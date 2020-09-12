using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Services.Abstracts {
    public class ApiServiceBase {

        #region {[ FIELDS ]}
        private ApiClient _client;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ApiServiceBase(ApiClient client) {
            _client = client;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        protected ApiClient Api() {
            return _client;
        }
        #endregion

    }
}
