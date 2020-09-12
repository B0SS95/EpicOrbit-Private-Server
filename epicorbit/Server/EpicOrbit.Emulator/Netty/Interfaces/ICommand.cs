namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface ICommand {

        short ID { get; }
        void Read(IDataInput param1, ICommandLookup lookup);
        void Write(IDataOutput param1);

    }
}
