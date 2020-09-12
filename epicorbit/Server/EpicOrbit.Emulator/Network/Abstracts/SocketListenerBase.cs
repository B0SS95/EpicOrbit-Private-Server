using EpicOrbit.Shared.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EpicOrbit.Emulator.Network.Abstracts {
    public abstract class SocketListenerBase {

        #region {[ PROPERTIES ]}
        public bool IsRunning { get => _isRunning; internal set => _isRunning = value; }
        #endregion

        #region {[ FIELDS ]}
        private IGameLogger _logger;
        private TcpListener _server;
        private CancellationTokenSource cancellationToken;
        private bool _isRunning;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public SocketListenerBase(IPEndPoint localEndpoint, int backlog) {
            try {
                _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");
                _server = new TcpListener(localEndpoint);

                _server.Start(backlog);
            } catch (Exception e) {
                _logger.LogError(e);
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void Start() {
            if (_isRunning) {
                return;
            }

            _isRunning = true;

            cancellationToken = new CancellationTokenSource();
            Task.Factory.StartNew(async () => {
                try {
                    _logger.LogSuccess($"SocketListener started listening on {_server.LocalEndpoint.ToString()}");
                    while (_isRunning) {
                        Socket socket = await _server.AcceptSocketAsync();
                        cancellationToken.Token.ThrowIfCancellationRequested();

                        _logger.LogInformation($"Client [{socket.RemoteEndPoint}] connected!");
                        await Accept(socket); // do not await, simply let the object do whatever it wants
                    }
                    _isRunning = false;
                } catch (Exception e) {
                    _logger.LogError(e);
                }
                _logger.LogWarning($"SocketListener stopped listening on {_server.LocalEndpoint.ToString()}");
            }, cancellationToken.Token);
        }

        public void Stop() {
            _isRunning = false;
            cancellationToken?.Cancel();
        }

        protected abstract Task Accept(Socket socket);
        #endregion

        #region {[ DESTRUCTOR ]}
        ~SocketListenerBase() {
            _logger.LogDebug("Destructor called!");
            _isRunning = false;
            _server.Stop();
        }
        #endregion

    }
}
