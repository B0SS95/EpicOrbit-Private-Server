using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;

namespace EpicOrbit.Emulator.Netty {
    public class NettyLookupBuilder : ILookupBuilder {

        private CommandLookup _commandLookup = null;
        public ICommandLookup BuildCommandLookup(IGameLogger logger) {
            if (_commandLookup == null) {
                _commandLookup = new CommandLookup(logger);
                int loaded = _commandLookup.LoadCommands<NettyLookupBuilder>("10.0.6435");

                if (loaded < 0) {
                    throw logger.LogError(new InvalidOperationException("Failed to fill commandLookup!"));
                }

                logger.LogInformation($"CommandLookup loaded with {loaded} commands!");
            }
            return _commandLookup;
        }

        private HandlerLookup _handlerLookup = null;
        public IHandlerLookup BuildHandlerLookup(IGameLogger logger) {
            if (_handlerLookup == null) {
                _handlerLookup = new HandlerLookup(logger);
                int loaded = _handlerLookup.LoadHandlers<NettyLookupBuilder>("10.0.6435");

                if (loaded < 0) {
                    throw logger.LogError(new InvalidOperationException("Failed to fill handlerLookup!"));
                }

                logger.LogInformation($"HandlerLookup loaded with {loaded} handlers!");
            }
            return _handlerLookup;
        }

    }
}
