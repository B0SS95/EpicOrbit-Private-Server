using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_786 : ICommand {

        public short ID { get; set; } = 25995;
        public double var_2860 = 0;
        public string name = "";
        public double var_3909 = 0;
        public int var_302 = 0;
        public int var_4302 = 0;
        public int var_4837 = 0;
        public int var_720 = 0;
        public int var_4777 = 0;

        public class_786(int param1 = 0, int param2 = 0, int param3 = 0, double param4 = 0, double param5 = 0, int param6 = 0, int param7 = 0, string param8 = "") {
            this.var_720 = param1;
            this.var_4777 = param2;
            this.var_4302 = param3;
            this.var_2860 = param4;
            this.var_3909 = param5;
            this.var_302 = param6;
            this.var_4837 = param7;
            this.name = param8;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2860 = param1.ReadDouble();
            this.name = param1.ReadUTF();
            this.var_3909 = param1.ReadDouble();
            this.var_302 = param1.ReadInt();
            this.var_302 = param1.Shift(this.var_302, 23);
            this.var_4302 = param1.ReadInt();
            this.var_4302 = param1.Shift(this.var_4302, 10);
            this.var_4837 = param1.ReadInt();
            this.var_4837 = param1.Shift(this.var_4837, 7);
            this.var_720 = param1.ReadInt();
            this.var_720 = param1.Shift(this.var_720, 31);
            this.var_4777 = param1.ReadInt();
            this.var_4777 = param1.Shift(this.var_4777, 6);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_2860);
            param1.WriteUTF(this.name);
            param1.WriteDouble(this.var_3909);
            param1.WriteInt(param1.Shift(this.var_302, 9));
            param1.WriteInt(param1.Shift(this.var_4302, 22));
            param1.WriteInt(param1.Shift(this.var_4837, 25));
            param1.WriteInt(param1.Shift(this.var_720, 1));
            param1.WriteInt(param1.Shift(this.var_4777, 26));
            param1.WriteShort(26939);
        }
    }
}
