using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Shared.Interfaces;

namespace EpicOrbit.Client {
    public class ClientContext {

        #region {[ STATIC FIELDS ]}
        private static IGameLogger _logger;
        private static string _host;
        #endregion

        #region {[ STATIC PROPERTIES ]}
        public static IGameLogger Logger => _logger;
        public static string Host => _host;
        #endregion

        #region {[ FUNCTIONS ]}
        public static void Initialize(IGameLogger logger, string host) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _host = host;
        }
        #endregion

    }
}
