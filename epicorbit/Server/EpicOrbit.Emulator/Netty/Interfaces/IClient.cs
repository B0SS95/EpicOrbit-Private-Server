using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Game.Controllers;
using System;
using System.Net;

namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface IClient : IDisposable {

        IGameLogger Logger { get; }
        long ConnectionID { get; }
        EndPoint EndPoint { get; }
        PlayerController Controller { get; }

        long BytesSent { get; }
        long BytesReceived { get; }

        void Send(ICommand commad);
        void Receive(byte[] data);

    }
}
