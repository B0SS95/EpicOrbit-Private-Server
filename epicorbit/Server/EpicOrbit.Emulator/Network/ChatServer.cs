using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Network.Handlers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using EpicOrbit.Emulator.Network.Abstracts;

namespace EpicOrbit.Emulator.Network {
    public class ChatServer : SocketListenerBase {

        #region {[ CONSTRUCTOR ]}
        public ChatServer(IPEndPoint options) : base(options, 100) { }
        #endregion

        #region {[ CALLBACK ]}
        protected override async Task Accept(Socket socket) {
            //   new ChatDebugConnectionHandler(socket);
            new ChatConnectionHandler(socket);
        }
        #endregion

    }
}
