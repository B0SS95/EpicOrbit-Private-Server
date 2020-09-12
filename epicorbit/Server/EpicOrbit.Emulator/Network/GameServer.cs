using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Network.Abstracts;
using EpicOrbit.Emulator.Network.Handlers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EpicOrbit.Emulator.Network {
    public class GameServer : SocketListenerBase {

        #region {[ CONSTRUCTOR ]}
        public GameServer(IPEndPoint options) : base(options, 100) {
        }
        #endregion

        #region {[ CALLBACK ]}
        protected override async Task Accept(Socket socket) {
            new GameConnectionHandler(socket);
        }
        #endregion

    }
}
