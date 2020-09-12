using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Extensions;
using EpicOrbit.Emulator;

namespace EpicOrbit.Server.Services {
    public class RessourceProvider {

        #region {[ PROPERTIES ]}
        public string Address { get; private set; }
        public double Reliability => (_checksCount - _failedChecksCount) / (1.0 * _checksCount);
        public bool Valid { get; private set; }
        public DateTime LastReachable { get; private set; }
        #endregion

        #region {[ FIELDS ]}
        private int _checksCount, _failedChecksCount;
        private string _token;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public RessourceProvider(string address, string token) {
            Address = address;
            _token = token;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public async Task CheckHealth() {
            _checksCount++;
            _failedChecksCount++;

            try {
                using (WebClient client = new WebClient()) {
                    if ((await client.DownloadDataTaskAsync($"http://{Address}/0000000000000000-0"))
                        .Is(_token.Hash())) {
                        LastReachable = DateTime.Now;
                        Valid = true;
                        _failedChecksCount--;
                        return;
                    }
                    Valid = false;
                }
            } catch (Exception e) {
                GameContext.Logger.LogWarning($"Provider: '{Address}', Error: '{e.Message}'");
            }
        }
        #endregion

    }
}
