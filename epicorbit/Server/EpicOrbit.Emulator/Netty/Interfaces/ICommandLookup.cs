namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface ICommandLookup {

        int LoadCommands<TCurrentClass>(string identifier);
        ICommand Lookup(IDataInput dataInput);

    }
}
