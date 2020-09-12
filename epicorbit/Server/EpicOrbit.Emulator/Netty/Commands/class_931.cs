using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_931 : ICommand {

        public short ID { get; set; } = 7114;
        public int var_2039 = 0;
        public int var_3307 = 0;
        public int var_4521 = 0;

        public class_931(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_3307 = param1;
            this.var_2039 = param2;
            this.var_4521 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2039 = param1.ReadInt();
            this.var_2039 = param1.Shift(this.var_2039, 5);
            param1.ReadShort();
            this.var_3307 = param1.ReadInt();
            this.var_3307 = param1.Shift(this.var_3307, 20);
            this.var_4521 = param1.ReadInt();
            this.var_4521 = param1.Shift(this.var_4521, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2039, 27));
            param1.WriteShort(20431);
            param1.WriteInt(param1.Shift(this.var_3307, 12));
            param1.WriteInt(param1.Shift(this.var_4521, 15));
        }
    }
}
