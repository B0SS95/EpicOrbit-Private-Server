using EpicOrbit.Shared.Interfaces;

namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface ILookupBuilder {

        ICommandLookup BuildCommandLookup(IGameLogger logger);
        IHandlerLookup BuildHandlerLookup(IGameLogger logger);

    }
}
