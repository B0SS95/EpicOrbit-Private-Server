using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Emulator;
using Newtonsoft.Json;

namespace EpicOrbit.Server.Services {
    public class RessourceProviderManager : IDisposable {

        #region {[ PROPERTIES ]}
        public string Token { get; private set; }
        public List<string> ValidProviders { get; private set; }
        #endregion

        #region {[ FIELDS ]}
        private Dictionary<string, RessourceProvider> _providers;
        private Timer _timer;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public RessourceProviderManager(string token) {
            Token = token;
            ValidProviders = new List<string>();

            Load();
            _timer = new Timer(x => {
                Update();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }


        #endregion

        #region {[ HELPERS ]}
        private void Save() {
            File.WriteAllText("providers.json", JsonConvert.SerializeObject(_providers.Keys.ToList()));
        }

        private void Load() {
            _providers = new Dictionary<string, RessourceProvider>();
            if (File.Exists("providers.json")) {
                foreach (string provider in
                    JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("providers.json"))) {
                    _providers.Add(provider, new RessourceProvider(provider, Token));
                }
            }
        }

        private async void Update() {
            Save();

            foreach (var item in _providers) {
                await item.Value.CheckHealth();
            }

            lock (ValidProviders) {
                ValidProviders.Clear();
                if (_providers.Count > 0) {
                    ValidProviders.AddRange(_providers.Values.Where(x => x.Valid 
                    && (DateTime.Now - x.LastReachable) < TimeSpan.FromMinutes(5)).Select(x => x.Address));
                }
            }

            GameContext.Logger.LogSuccess("Health checked!");
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public Dictionary<string, double> Get() {
            return _providers.ToDictionary(x => x.Key, x => x.Value.Reliability);
        }

        public void Add(string provider) {
            _providers[provider] = new RessourceProvider(provider, Token);
        }

        public void Delete(string provider) {
            _providers.Remove(provider);
        }
        #endregion

        #region {[ IDISPOSABLE ]}
        public void Dispose() {
            _timer.Dispose();
            Save();
        }

        ~RessourceProviderManager() {
            Dispose();
        }
        #endregion

    }
}
