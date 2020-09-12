using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_849 : ICommand {

        public short ID { get; set; } = 6592;
        public bool var_3101 = false;
        public int var_2896 = 0;

        public class_849(bool param1 = false, int param2 = 0) {
            this.var_3101 = param1;
            this.var_2896 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_3101 = param1.ReadBoolean();
            this.var_2896 = param1.ReadInt();
            this.var_2896 = param1.Shift(this.var_2896, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(20334);
            param1.WriteBoolean(this.var_3101);
            param1.WriteInt(param1.Shift(this.var_2896, 24));
        }
    }
}
