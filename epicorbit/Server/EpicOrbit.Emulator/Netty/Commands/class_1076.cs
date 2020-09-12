using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1076 : ICommand {

        public short ID { get; set; } = 24356;
        public int var_323 = 0;
        public int var_1514 = 0;

        public class_1076(int param1 = 0, int param2 = 0) {
            this.var_1514 = param1;
            this.var_323 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_323 = param1.ReadInt();
            this.var_323 = param1.Shift(this.var_323, 22);
            this.var_1514 = param1.ReadInt();
            this.var_1514 = param1.Shift(this.var_1514, 25);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_323, 10));
            param1.WriteInt(param1.Shift(this.var_1514, 7));
        }
    }
}
