using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LogoutCommand : ICommand {

        public const short LOGOUT_DONE = 0;
        public const short LOGOUT_CANCELLED = 1;
        public short ID { get; set; } = 15460;
        public short command = 0;

        public LogoutCommand(short param1 = 0) {
            this.command = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.command = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.command);
            param1.WriteShort(-2492);
        }
    }
}
