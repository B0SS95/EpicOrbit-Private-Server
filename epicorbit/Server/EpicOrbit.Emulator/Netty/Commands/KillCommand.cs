using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class KillCommand : ICommand {

        public short ID { get; set; } = 10484;
        public int var_1090 = 0;
        public int userId = 0;

        public KillCommand(int param1 = 0, int param2 = 0) {
            this.userId = param1; // uid
            this.var_1090 = param2; // not used
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1090 = param1.ReadInt();
            this.var_1090 = param1.Shift(this.var_1090, 12);
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1090, 20));
            param1.WriteInt(param1.Shift(this.userId, 15));
        }
    }
}
