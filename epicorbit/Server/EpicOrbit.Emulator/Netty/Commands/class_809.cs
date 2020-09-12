using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_809 : ICommand {

        public short ID { get; set; } = 24946;
        public double var_3155 = 0;
        public short var_3542 = 0;
        public short var_3673 = 0;
        public short name_171 = 0;

        public class_809(short param1 = 0, double param2 = 0, short param3 = 0, short param4 = 0) {
            this.var_3673 = param1;
            this.var_3155 = param2;
            this.name_171 = param3;
            this.var_3542 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3155 = param1.ReadDouble();
            param1.ReadShort();
            this.var_3542 = param1.ReadShort();
            this.var_3673 = param1.ReadShort();
            param1.ReadShort();
            this.name_171 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_3155);
            param1.WriteShort(-11584);
            param1.WriteShort(this.var_3542);
            param1.WriteShort(this.var_3673);
            param1.WriteShort(-8372);
            param1.WriteShort(this.name_171);
        }
    }
}
