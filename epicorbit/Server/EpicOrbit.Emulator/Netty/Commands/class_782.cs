using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_782 : ICommand {

        public short ID { get; set; } = 20596;
        public int var_830 = 0;
        public int var_3910 = 0;
        public FactionModule var_964;
        public int var_1917 = 0;
        public FactionModule var_4596;
        public int var_2803 = 0;

        public class_782(FactionModule param1 = null, FactionModule param2 = null, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0) {
            if (param1 == null) {
                this.var_4596 = new FactionModule();
            } else {
                this.var_4596 = param1;
            }
            if (param2 == null) {
                this.var_964 = new FactionModule();
            } else {
                this.var_964 = param2;
            }
            this.var_2803 = param3;
            this.var_1917 = param4;
            this.var_830 = param5;
            this.var_3910 = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_830 = param1.ReadInt();
            this.var_830 = param1.Shift(this.var_830, 8);
            param1.ReadShort();
            this.var_3910 = param1.ReadInt();
            this.var_3910 = param1.Shift(this.var_3910, 10);
            this.var_964 = lookup.Lookup(param1) as FactionModule;
            this.var_964.Read(param1, lookup);
            this.var_1917 = param1.ReadInt();
            this.var_1917 = param1.Shift(this.var_1917, 8);
            this.var_4596 = lookup.Lookup(param1) as FactionModule;
            this.var_4596.Read(param1, lookup);
            this.var_2803 = param1.ReadInt();
            this.var_2803 = param1.Shift(this.var_2803, 6);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_830, 24));
            param1.WriteShort(-705);
            param1.WriteInt(param1.Shift(this.var_3910, 22));
            this.var_964.Write(param1);
            param1.WriteInt(param1.Shift(this.var_1917, 24));
            this.var_4596.Write(param1);
            param1.WriteInt(param1.Shift(this.var_2803, 26));
        }
    }
}
