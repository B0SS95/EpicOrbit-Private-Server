using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Network.Abstracts;
using EpicOrbit.Emulator.Network.Handlers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EpicOrbit.Emulator.Network {
    public class PolicyServer : SocketListenerBase {

        #region {[ CONSTRUCTOR ]}
        public PolicyServer(IPEndPoint options) : base(options, 100) {
        }
        #endregion

        #region {[ CALLBACK ]}
        protected override async Task Accept(Socket socket) {
            new PolicyConnectionHandler(socket);
        }
        #endregion

    }
}
