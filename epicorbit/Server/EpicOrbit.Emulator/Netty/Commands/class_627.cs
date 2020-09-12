using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_627 : ICommand {

        public short ID { get; set; } = 31754;
        public int var_1679 = 0;
        public int name_101 = 0;

        public class_627(int param1 = 0, int param2 = 0) {
            this.var_1679 = param1;
            this.name_101 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1679 = param1.ReadInt();
            this.var_1679 = param1.Shift(this.var_1679, 22);
            this.name_101 = param1.ReadInt();
            this.name_101 = param1.Shift(this.name_101, 17);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1679, 10));
            param1.WriteInt(param1.Shift(this.name_101, 15));
            param1.WriteShort(4126);
        }
    }
}
