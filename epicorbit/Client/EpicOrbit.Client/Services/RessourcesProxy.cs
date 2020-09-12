using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EpicOrbit.Client.Services.Extensions;
using EpicOrbit.Client.Services.Implementations;
using EpicOrbit.Shared.Extensions;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Ressources;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace EpicOrbit.Client.Services {
    public class RessourcesProxy {

        #region {[ STATIC ]}
        private static Random _random = new Random();
        #endregion

        #region {[ PROPERTIES ]}
        private List<string> Providers { get; set; } = new List<string>();
        private RessourceTableView Ressources { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private byte[] _token;
        private bool _keyRetrieved;
        private bool _ressourcesRetrieved;
        private Timer _timer;
        private IDataProtector _protector;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public RessourcesProxy(IDataProtectionProvider provider) {
            _protector = provider.CreateProtector("ressourceproxy");
            _timer = new Timer(x => {
                UpdateProviders();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }
        #endregion

        #region {[ HELPERS ]}
        private string ByteArrayToString(byte[] ba) {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        private string RandomProvider() {
            lock (Providers) {
                return Providers[_random.Next(Providers.Count)];
            }
        }

        private string Unprotect(byte[] @protected) {
            return Encoding.UTF8.GetString(_protector.Unprotect(@protected));
        }

        private byte[] Protect(string unprotected) {
            return _protector.Protect(Encoding.UTF8.GetBytes(unprotected));
        }

        private void RetrieveKey(int counter = 0) {
            if (counter == 3) {
                ClientContext.Logger.LogWarning($"Failed to reach API! ({counter} retries!)", "", "", 0);
                return;
            }

            try {
                Tuple<string, string> tempKey = SecurityExtension.CreatePair();
                ApiClient.CreateRequest("api/ressources/token", "POST")
                    .WithBody(new TokenView { Token = tempKey.Item2 })
                    .Execute(out HttpWebResponse response);
                if (response.TryGetStatusCode(out HttpStatusCode code)) {
                    string responseString = response.GetReponseString();
                    if (response.StatusCode == HttpStatusCode.OK) {
                        TokenView tokenView = responseString.DeserializeJsonSafe<TokenView>();
                        if (tokenView != null) {
                            _token = Protect(SecurityExtension.Decrypt(tokenView.Token, tempKey.Item1));
                            _keyRetrieved = true;
                            ClientContext.Logger.LogSuccess("Token retrieved successfully!", "", "", 0);
                            return;
                        }
                        ClientContext.Logger.LogWarning($"Failed to extract deserialize response ['{responseString}']", "", "", 0);
                    } else {
                        ClientContext.Logger.LogWarning($"Request failed [Code: {response.StatusCode.ToString("G")}, Response: '{responseString}']", "", "", 0);
                    }
                }
            } catch (Exception e) {
                ClientContext.Logger.LogError(e, "", "", 0);
            }

            RetrieveKey(++counter);
        }

        private void RetrieveRessourceTable(int counter = 0) {
            if (!_keyRetrieved) {
                RetrieveKey();
            }

            if (counter == 3) {
                ClientContext.Logger.LogWarning($"Failed to reach API! ({counter} retries!)", "", "", 0);
                return;
            }

            try {
                using (WebClient webClient = new WebClient())
                using (MemoryStream input = new MemoryStream(webClient.DownloadData($"http://{RandomProvider()}/0000000000000000-1")))
                using (MemoryStream output = new MemoryStream()) {
                    SecurityExtension.DecryptAES(input, output, Unprotect(_token));
                    output.Position = 0;

                    Ressources = JsonConvert.DeserializeObject<RessourceTableView>(Encoding.UTF8.GetString(output.ToArray()));
                    _ressourcesRetrieved = true;
                    return;
                }
            } catch (Exception e) {
                ClientContext.Logger.LogError(e);
            }

            RetrieveRessourceTable(++counter);
        }

        private void UpdateProviders(int counter = 0) {
            if (counter == 3) {
                ClientContext.Logger.LogWarning($"Failed to reach API! ({counter} retries!)");
                return;
            }

            try {
                ApiClient.CreateRequest("api/ressources", "GET").Execute(out HttpWebResponse response);
                if (response.TryGetStatusCode(out HttpStatusCode code)) {
                    string responseString = response.GetReponseString();
                    if (response.StatusCode == HttpStatusCode.OK) {
                        RessourcesProviderView ressourcesProviderView = responseString.DeserializeJsonSafe<RessourcesProviderView>();
                        if (ressourcesProviderView != null) {
                            lock (Providers) {
                                Providers.Clear();
                                Providers.AddRange(ressourcesProviderView.Providers);
                            }
                            ClientContext.Logger.LogSuccess($"Providers updated successfully! (Count: {ressourcesProviderView.Providers.Count})");
                            return;
                        }
                        ClientContext.Logger.LogWarning($"Failed to extract deserialize response ['{responseString}']");
                    } else {
                        ClientContext.Logger.LogWarning($"Montitor request failed [Code: {response.StatusCode.ToString("G")}, Response: '{responseString}']");
                    }
                }
            } catch (Exception e) {
                ClientContext.Logger.LogError(e);
            }

            UpdateProviders(++counter);
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public async Task Retrieve(string ressource, Stream target, int counter = 0) {
            if (!_keyRetrieved) {
                RetrieveKey();
            }

            if (!_ressourcesRetrieved) {
                RetrieveRessourceTable();
            }

            if (counter == 3) {
                ClientContext.Logger.LogWarning($"Failed to retrieve ressource '{ressource}'! ({counter} retries!)");
                return;
            }

            try {
                string res = ByteArrayToString(Unprotect(_token).Hash(ressource));

                if (!Ressources.Table.TryGetValue(res, out RessourceTableItemView itemView)) {
                    ClientContext.Logger.LogInformation($"File {ressource} does not exsit in the ressource table!");
                    return;
                }

                using (WebClient webClient = new WebClientWithRange(itemView.Offset, itemView.Offset + (itemView.Length - 1)))
                using (MemoryStream input = new MemoryStream(await webClient.DownloadDataTaskAsync($"http://{RandomProvider()}/0000000000000000-{itemView.Container}")))
                using (MemoryStream output = new MemoryStream()) {
                    SecurityExtension.DecryptAES(input, output, Unprotect(_token));
                    output.Position = 0;

                    using (GZipStream decompressor = new GZipStream(output, CompressionMode.Decompress, true)) {
                        decompressor.CopyTo(target);
                    }
                }
            } catch (Exception e) {
                ClientContext.Logger.LogError(e);
                await Retrieve(ressource, target, ++counter);
            }
        }
        #endregion

    }
}
