using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_855 : ICommand {

        public short ID { get; set; } = 26225;
        public int var_2995 = 0;
        public int name_95 = 0;
        public int name_123 = 0;

        public class_855(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_2995 = param1;
            this.name_123 = param2;
            this.name_95 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2995 = param1.ReadInt();
            this.var_2995 = param1.Shift(this.var_2995, 13);
            this.name_95 = param1.ReadInt();
            this.name_95 = param1.Shift(this.name_95, 13);
            param1.ReadShort();
            this.name_123 = param1.ReadInt();
            this.name_123 = param1.Shift(this.name_123, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2995, 19));
            param1.WriteInt(param1.Shift(this.name_95, 19));
            param1.WriteShort(-7686);
            param1.WriteInt(param1.Shift(this.name_123, 13));
        }
    }
}
