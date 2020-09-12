using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class UIWindowSelectRequestHandler : ICommandHandler<UIWindowSelectRequest> {
        public void Execute(IClient initiator, UIWindowSelectRequest command) {
            switch (command.itemId) {
                case "logout":
                    initiator.Controller.Logout();
                    break;
            }
        }
    }

}
