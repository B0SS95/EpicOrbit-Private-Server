using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_916 : ICommand {

        public short ID { get; set; } = 31019;
        public double var_4995 = 0;
        public double var_3884 = 0;
        public int var_4133 = 0;
        public double var_1970 = 0;

        public class_916(double param1 = 0, double param2 = 0, double param3 = 0, int param4 = 0) {
            this.var_4995 = param1;
            this.var_3884 = param2;
            this.var_1970 = param3;
            this.var_4133 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4995 = param1.ReadDouble();
            param1.ReadShort();
            this.var_3884 = param1.ReadDouble();
            this.var_4133 = param1.ReadInt();
            this.var_4133 = param1.Shift(this.var_4133, 1);
            this.var_1970 = param1.ReadDouble();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_4995);
            param1.WriteShort(17893);
            param1.WriteDouble(this.var_3884);
            param1.WriteInt(param1.Shift(this.var_4133, 31));
            param1.WriteDouble(this.var_1970);
            param1.WriteShort(7710);
        }
    }
}
