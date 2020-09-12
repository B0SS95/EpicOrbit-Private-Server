namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface IHandlerLookup {

        int LoadHandlers<TCurrentClass>(string identifier);
        void Handle(ICommand command, IClient initiator);

    }
}
