using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Implementations;
using System;
using System.Linq;
using System.Net.Sockets;

namespace EpicOrbit.Emulator.Network.Handlers {
    public class ChatDebugConnectionHandler : IDisposable {

        #region {[ STATIC PROPERTIES ]}
        public static short BufferSize { get; set; } = 1024;
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private NetworkStream _stream;
        private Socket _socket;
        #endregion

        private TcpClient _client;

        #region {[ CONSTRUCTOR ]}
        public ChatDebugConnectionHandler(Socket socket) {
            _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");
            _socket = socket ?? throw _logger.LogError(new ArgumentNullException(nameof(socket)));
            _stream = new NetworkStream(_socket);


            _client = new TcpClient();
            _client.Connect("eps.optimist.darkorbit.infinicast.io", 7771);


            Process("Client_0", _stream);
            Process("Server_0", _client.GetStream());
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public async void Process(string sender, NetworkStream stream) {
            try {
                byte[] buffer = new byte[BufferSize];
                int read = 0;
                while (true) {
                    byte[] data = new byte[0];
                    while (data.Length < 2) { //wir müssen so lange lesen, bis wir 2 bytes haben
                        read = await stream.ReadAsync(buffer, 0, 2 - data.Length);
                        if (read == 0) {
                            return;
                        }

                        data = data.Merge(buffer.SubArray(0, read));
                    }

                    if (data[0] != 42) {
                        while (data.Length < 5) { //wir müssen so lange lesen, bis wir 5 bytes haben
                            read = await stream.ReadAsync(buffer, 0, 5 - data.Length);
                            if (read == 0) {
                                return;
                            }

                            data = data.Merge(buffer.SubArray(0, read));
                        }

                        int length = BitConverter.ToInt32(data.SubArray(1, 4).Reverse().ToArray(), 0);

                        byte[] payload = new byte[length];
                        int alreadyRead = 0;
                        while (alreadyRead < length) {
                            read = await stream.ReadAsync(buffer, 0, Math.Min(buffer.Length, length - alreadyRead));
                            if (read == 0) {
                                return;
                            }

                            payload.IntelligentMerge(buffer.SubArray(0, read), alreadyRead);
                            alreadyRead += read;
                        }

                        data = data.Merge(payload);
                    }

                    Handle(sender, data);
                }
            } catch { }
            //    Dispose:
            //  Dispose();
        }
        #endregion

        #region {[ HELPER ]}
        private void Handle(string sender, byte[] data) {
            try {

                object message = APlayProtocolDecoder.Decode(new BinaryInputStream(data));

                //    if (sender == "CLIENT_0" && message is APlayStringMessage msg) {
                //        msg.GetDataAsDecodedJson()["path"] = ((string)msg.GetDataAsDecodedJson()["path"]).Replace("❌ＦＬＯＷＦＥＨＬＥＲ❌", "ALPHA");
                //    }

                _logger.LogSuccess($"[{sender}] [ID: {data[0]}] Message processed: {message?.GetType().Name ?? "null"}");

                if (message != null) {
                    _logger.LogCritical(Newtonsoft.Json.JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented));
                }

                data = APlayProtocolEncoder.Encode(message);

                if (sender == "Client_0") {
                    _client.GetStream().Write(data, 0, data.Length);
                } else {
                    _stream.Write(data, 0, data.Length);
                }

            } catch (Exception e) {
                _logger.LogError(e);
            }
        }
        #endregion

        #region {[ DISPOSE & DESTRUCTOR ]}
        public void Dispose() {
            _socket.Close();
        }

        ~ChatDebugConnectionHandler() {
            _logger.LogDebug("Destructor called!");
            Dispose();
        }
        #endregion
    }
}
