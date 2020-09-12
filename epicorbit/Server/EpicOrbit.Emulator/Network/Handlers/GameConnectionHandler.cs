using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Server.Data.Implementations;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Netty.Implementations;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace EpicOrbit.Emulator.Network.Handlers {
    public class GameConnectionHandler : IClient {

        #region {[ STATIC HELPER ]}
        private short ToShort(byte[] data, int position) {
            int ch1 = data[position + 0];
            int ch2 = data[position + 1];
            if ((ch1 | ch2) < 0) {
                return 0;
            }

            return (short)((ch1 << 8) + (ch2 << 0));
        }

        private byte[] FromShort(short value) {
            return new byte[] {
                (byte)(((uint)value >> 8) & 0xFF),
                (byte)(((uint)value >> 0) & 0xFF)
            };
        }
        #endregion

        #region {[ STATIC PROPERTIES ]}
        public static short BufferSize { get; set; } = 1024;
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private NetworkStream _stream;
        private Socket _socket;

        private IHandlerLookup _handlerLookup;
        private ICommandLookup _commandLookup;
        #endregion

        #region {[ PROPERTIES ]}
        public PlayerController Controller { get; set; }
        public IGameLogger Logger => _logger;
        public long ConnectionID { get; } = BitConverter.ToInt64(RandomGenerator.Bytes(8), 0);
        public EndPoint EndPoint => _socket.LocalEndPoint;
        public long BytesSent { get; private set; }
        public long BytesReceived { get; private set; }
        public bool IsDisposed => _disposed;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public GameConnectionHandler(Socket socket) {
            _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");
            _socket = socket ?? throw _logger.LogError(new ArgumentNullException(nameof(socket)));
            _stream = new NetworkStream(_socket);

            _handlerLookup = GameContext.LookupBuilder.BuildHandlerLookup(_logger);
            _commandLookup = GameContext.LookupBuilder.BuildCommandLookup(_logger);

            Process();
        }
        #endregion

        #region {[ HELPER ]}
        public void Receive(byte[] data) { 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try {
                IDataInput dataInput = new DataInputStream(data);
                ICommand command = _commandLookup.Lookup(dataInput);
                command.Read(dataInput, _commandLookup);

                _handlerLookup.Handle(command, this);
            } catch (Exception e) {
                _logger.LogError(e);
            }
            sw.Stop();
            _logger.LogDebug($"Execution took {sw.ElapsedMilliseconds}ms!");
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

                    short length = ToShort(data, 0);
                    if (length <= 0) {
                        continue;
                    }

                    data = new byte[length];
                    int alreadyRead = 0;

                    while (alreadyRead < length) {
                        read = await _stream.ReadAsync(buffer, 0, Math.Min(buffer.Length, length - alreadyRead));
                        if (read == 0) {
                            goto Dispose;
                        }

                        data.IntelligentMerge(buffer.SubArray(0, read), alreadyRead);
                        alreadyRead += read;
                    }

                    BytesReceived += data.Length + 2;
                    Receive(data);
                }
            } catch { }
            Dispose: Dispose();
        }

        public async void Send(ICommand command) {
            try {
                DataOutputStream outputStream = new DataOutputStream();
                command.Write(outputStream);

                byte[] binary = outputStream.GetData();
                await _stream.WriteAsync(binary, 0, binary.Length);
            } catch (Exception e) {
                _logger.LogError(e);
            }
        }

        public async void Send(IEnumerable<ICommand> commands) {
            try {
                List<byte[]> rawCommands = new List<byte[]>();
                int sum = 0;

                foreach (ICommand command in commands) {
                    if (command == null) {
                        continue;
                    }

                    DataOutputStream outputStream = new DataOutputStream();
                    command.Write(outputStream);

                    byte[] temp = outputStream.GetData();
                    sum += temp.Length;

                    rawCommands.Add(temp);
                }

                byte[] binary = new byte[sum];
                int position = 0;
                foreach (byte[] raw in rawCommands) {
                    binary.IntelligentMerge(raw, position);
                    position += raw.Length;
                }

                await _stream.WriteAsync(binary, 0, binary.Length);
            } catch (Exception e) {
                _logger.LogError(e);
            }
        }
        #endregion

        #region {[ DISPOSE & DESTRUCTOR ]}
        private bool _disposed = false;

        public void Dispose(bool logout) {
            if (_disposed || !(_disposed = true)) {
                return;
            }

            if (logout && Controller != null) {
                Controller.Logout(true);
            }

            _logger.LogInformation($"Client [{_socket.RemoteEndPoint}] disconnected!");
            _socket.Close();
        }


        public void Dispose() {
            Dispose(true);
        }

        ~GameConnectionHandler() {
            _logger.LogDebug("Destructor called!");
            Dispose();
        }
        #endregion

    }
}
