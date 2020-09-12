using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Emulator.Network;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Server.Data.Repositories;
using EpicOrbit.Server.Data.Repositories.Interfaces;

namespace EpicOrbit.Emulator {
    public static class GameContext {

        #region {[ STATIC FIELDS ]}
        private static IGameLogger _logger;
        private static ILookupBuilder _lookupBuilder;
        private static IDatabaseContext _database;

        private static GameServer _gameServer;
        private static PolicyServer _policyServer;
        private static ChatServer _chatServer;

        private static bool _initialized;
        private static object _lock = new object();
        #endregion

        #region {[ STATIC PROPERTIES ]}
        public static IGameLogger Logger => _logger;
        public static ILookupBuilder LookupBuilder => _lookupBuilder;
        public static IDatabaseContext Database => _database;
        public static bool IsRunning => _initialized && (_gameServer.IsRunning && _policyServer.IsRunning);
        #endregion

        #region {[ FUNCTIONS ]}
        public static void Initialize(IGameLogger logger, string connectionString, string database,
            IPEndPoint gameServerConfiguration, IPEndPoint policyServerConfiguration,
            IPEndPoint chatServerConfiguration) {
            lock (_lock) {
                if (_initialized || !(_initialized = true)) {
                    return;
                }

                _logger = logger ?? throw new ArgumentNullException(nameof(logger));

                _database = new DatabaseContext(connectionString, database);
                _lookupBuilder = new NettyLookupBuilder();
                _gameServer = new GameServer(gameServerConfiguration);
                _policyServer = new PolicyServer(policyServerConfiguration);
                _chatServer = new ChatServer(chatServerConfiguration);
            }
        }

        public static void Start() {
            lock (_lock) {
                if (!_initialized) {
                    return;
                }

                _gameServer.Start();
                _policyServer.Start();
                _chatServer.Start();
            }
        }

        public static void Stop() {
            lock (_lock) {
                if (!_initialized) {
                    return;
                }

                _gameServer.Stop();
                _policyServer.Stop();
                _chatServer.Stop();
            }
        }
        #endregion

    }
}
