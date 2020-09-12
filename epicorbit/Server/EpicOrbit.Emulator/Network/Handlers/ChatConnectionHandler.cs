using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Chat;
using EpicOrbit.Emulator.Chat.Controllers;
using EpicOrbit.Emulator.Chat.Infinicast.Messages;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Implementations;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Sockets;

namespace EpicOrbit.Emulator.Network.Handlers {
    public class ChatConnectionHandler : IDisposable {

        #region {[ STATIC PROPERTIES ]}
        public static short BufferSize { get; set; } = 1024;
        #endregion

        #region {[ PROPERTIES ]}
        public ChatUserController Controller { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private NetworkStream _stream;
        private Socket _socket;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ChatConnectionHandler(Socket socket) {
            _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");
            _socket = socket ?? throw _logger.LogError(new ArgumentNullException(nameof(socket)));
            _stream = new NetworkStream(_socket);


            Process();
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public async void Process() {
            try {
                byte[] buffer = new byte[BufferSize];
                int read = 0;
                while (true) {
                    byte[] data = new byte[0];
                    while (data.Length < 2) { //wir müssen so lange lesen, bis wir 2 bytes haben
                        read = await _stream.ReadAsync(buffer, 0, 2 - data.Length);
                        if (read == 0) {
                            goto Dispose;
                        }

                        data = data.Merge(buffer.SubArray(0, read));
                    }

                    if (data[0] != 42) {
                        while (data.Length < 5) { //wir müssen so lange lesen, bis wir 5 bytes haben
                            read = await _stream.ReadAsync(buffer, 0, 5 - data.Length);
                            if (read == 0) {
                                goto Dispose;
                            }

                            data = data.Merge(buffer.SubArray(0, read));
                        }

                        int length = BitConverter.ToInt32(data.SubArray(1, 4).Reverse().ToArray(), 0);

                        byte[] payload = new byte[length];
                        int alreadyRead = 0;
                        while (alreadyRead < length) {
                            read = await _stream.ReadAsync(buffer, 0, Math.Min(buffer.Length, length - alreadyRead));
                            if (read == 0) {
                                goto Dispose;
                            }

                            payload.IntelligentMerge(buffer.SubArray(0, read), alreadyRead);
                            alreadyRead += read;
                        }

                        data = data.Merge(payload);
                    }

                    Receive(data);
                }
            } catch { }
            Dispose:
            Dispose();
        }

        public async void Send(object message) {
            try {

                //         Console.WriteLine(JsonConvert.SerializeObject(message));
                byte[] binary = APlayProtocolEncoder.Encode(message);

                if (binary == null || binary.Length == 0) {
                    _logger.LogDebug($"Encoded is null!");
                    return;
                }

                await _stream.WriteAsync(binary, 0, binary.Length);
            } catch (Exception e) {
                _logger.LogError(e);
            }
        }

        public void SendJson(dynamic message) {
            Send(new APlayStringMessage() { DecodedData = JObject.FromObject(message) });
        }
        #endregion

        #region {[ HELPER ]}
        private void Receive(byte[] data) {
            try {

                object message = APlayProtocolDecoder.Decode(new BinaryInputStream(data));
                _logger.LogDebug($"[ID: {data[0]}] [Type: {message?.GetType().Name ?? "null"}]: " +
                    $"{(message == null ? "null" : Newtonsoft.Json.JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented))}");

                if (message != null) {
                    Handle(message);
                }

            } catch (Exception e) {
                _logger.LogError(e);
            }
        }

        private void Handle(object message) {
            switch (message) {
                case LowLevelConnectMessage connectMessage:
                    Send(new LowLevelIntroductionMessage { AddressString = "127.0.0.1:9338" });
                    break;
                case APlayStringMessage stringMessage:
                    StringMessageHandler(stringMessage.DecodedData);
                    break;
                default:
                    _logger.LogWarning($"Handler for '{message.GetType().Name}' not found!");
                    break;
            }
        }

        private void StringMessageHandler(JObject message) {
            if (message["path"] != null) {

                bool handled = HandleServerStatusAndPredefinedRoomsCommand(message)
                            || HandleWhisperCommand(message);

            } else {
                if (message["role"] != null && message["space"] != null && message["data"]?["version"] != null) { // Initialize Chat
                    SendJson(ChatPacketBuilder.ChatInitializationCommand());
                }
            }
        }
        #endregion

        #region {[ HANDLER ]}

        private bool HandleServerStatusAndPredefinedRoomsCommand(JObject message) {
            if ((string)message["path"] != "/~endpoints/EpicOrbit/serverStatus/" && (string)message["path"] != "/~endpoints/EpicOrbit/changePredefinedRoom/") {
                return false;
            }

            if (message["requestId"] == null) {
                _logger.LogWarning("Message does not provide a requestId!");
                return true;
            }

            SendJson(ChatPacketBuilder.BasicCommand(new { }, 2, int.Parse((string)message["requestId"]), new { }));

            if (((string)message["path"]).EndsWith("serverStatus/")) {
                SendJson(ChatPacketBuilder.BasicCommand((string)message["path"], new {
                    wordFilter = new object[0],
                    type = "loggedIn"
                }, 8, new { }));
            }

            return true;
        }


        private bool HandleWhisperCommand(JObject message) {
            if (!((string)message["path"]).StartsWith("/whisper/69")) {
                return false;
            }

            string globalUserId = ((string)message["path"]).Split('/')[2];


            SendJson(ChatPacketBuilder.CreateRoomCommand("69_Global_en", "GLOBAL", "sys"));
            SendJson(ChatPacketBuilder.CreateRoomCommand("69_MMO_en", "FACTION", "sys"));

            return true;
        }
        #endregion

        #region {[ DISPOSE & DESTRUCTOR ]}
        private bool _disposed = false;

        public void Dispose(bool logout) {
            if (_disposed || !(_disposed = true)) {
                return;
            }

            if (logout && Controller != null) {
                Controller.Logout();
            }

            _logger.LogInformation($"Client [{_socket.RemoteEndPoint}] disconnected!");
            _socket.Close();
        }


        public void Dispose() {
            Dispose(true);
        }

        ~ChatConnectionHandler() {
            _logger.LogDebug("Destructor called!");
            Dispose();
        }
        #endregion
    }
}
