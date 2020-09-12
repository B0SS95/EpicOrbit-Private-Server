using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1051 : ICommand {

        public short ID { get; set; } = 7102;
        public int var_2607 = 0;
        public int name_37 = 0;

        public class_1051(int param1 = 0, int param2 = 0) {
            this.var_2607 = param1;
            this.name_37 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2607 = param1.ReadInt();
            this.var_2607 = param1.Shift(this.var_2607, 9);
            this.name_37 = param1.ReadInt();
            this.name_37 = param1.Shift(this.name_37, 23);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2607, 23));
            param1.WriteInt(param1.Shift(this.name_37, 9));
        }
    }
}
