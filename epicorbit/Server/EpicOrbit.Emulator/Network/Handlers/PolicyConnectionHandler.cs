using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;

namespace EpicOrbit.Emulator.Network.Handlers {
    public class PolicyConnectionHandler : IDisposable {

        #region {[ STATIC ]}
        internal static byte[] PolicyFile = Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?><cross-domain-policy xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"http://www.adobe.com/xml/schemas/PolicyFileSocket.xsd\"><allow-access-from domain=\"*\" to-ports=\"*\" secure=\"false\" /><site-control permitted-cross-domain-policies=\"master-only\" /></cross-domain-policy>" + '\u0000');
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private NetworkStream _stream;
        private Socket _socket;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PolicyConnectionHandler(Socket socket) {
            _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");
            _socket = socket ?? throw _logger.LogError(new ArgumentNullException(nameof(socket)));
            _stream = new NetworkStream(_socket);

            Process();
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public async void Process() {
            try {
                byte[] data = new byte[0];
                byte[] buffer = new byte[1];
                while (await _stream.ReadAsync(buffer, 0, buffer.Length) > 0) {
                    if (buffer[0] != '\u0000' && buffer[0] != '\n' && buffer[0] != '\r') {
                        data = data.Merge(buffer);
                    } else {
                        string packet = Encoding.UTF8.GetString(data);
                        if (!string.IsNullOrWhiteSpace(packet) && packet.Equals("<policy-file-request/>")) {
                            await _stream.WriteAsync(PolicyFile, 0, PolicyFile.Length);
                            _logger.LogDebug($"Policy file sent to { _socket.RemoteEndPoint.ToString() }!");
                            break;
                        }
                    }
                }
            } catch { }
            Dispose();
        }
        #endregion

        #region {[ DISPOSE & DESTRUCTOR ]}
        public void Dispose() {
            _socket.Close();
        }

        ~PolicyConnectionHandler() {
            _logger.LogDebug("Destructor called!");
            Dispose();
        }
        #endregion

    }
}
