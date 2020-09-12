namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface ICommandHandler<T> where T : ICommand {

        void Execute(IClient initiator, T command);

    }
}
