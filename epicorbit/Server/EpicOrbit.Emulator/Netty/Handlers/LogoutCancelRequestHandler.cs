using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class LogoutCancelRequestHandler : ICommandHandler<LogoutCancelRequest> {
        public void Execute(IClient initiator, LogoutCancelRequest command) {
            initiator.Controller.CancelLogout();
        }
    }

}
