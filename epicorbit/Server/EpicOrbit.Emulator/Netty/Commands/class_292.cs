using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_292 : ICommand {

        public const short const_2883 = 1;
        public const short const_2281 = 0;
        public short ID { get; set; } = 2746;
        public int var_923 = 0;
        public short var_546 = 0;
        public FactionModule opponent;
        public int name_99 = 0;
        public int var_2726 = 0;
        public int var_3605 = 0;
        public int var_3932 = 0;
        public int var_2050 = 0;

        public class_292(int param1 = 0, FactionModule param2 = null, short param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0) {
            this.name_99 = param1;
            if (param2 == null) {
                this.opponent = new FactionModule();
            } else {
                this.opponent = param2;
            }
            this.var_546 = param3;
            this.var_3605 = param4;
            this.var_923 = param5;
            this.var_2726 = param6;
            this.var_2050 = param7;
            this.var_3932 = param8;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_923 = param1.ReadInt();
            this.var_923 = param1.Shift(this.var_923, 29);
            this.var_546 = param1.ReadShort();
            this.opponent = lookup.Lookup(param1) as FactionModule;
            this.opponent.Read(param1, lookup);
            this.name_99 = param1.ReadInt();
            this.name_99 = param1.Shift(this.name_99, 22);
            this.var_2726 = param1.ReadInt();
            this.var_2726 = param1.Shift(this.var_2726, 1);
            this.var_3605 = param1.ReadInt();
            this.var_3605 = param1.Shift(this.var_3605, 14);
            this.var_3932 = param1.ReadInt();
            this.var_3932 = param1.Shift(this.var_3932, 12);
            this.var_2050 = param1.ReadInt();
            this.var_2050 = param1.Shift(this.var_2050, 2);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_923, 3));
            param1.WriteShort(this.var_546);
            this.opponent.Write(param1);
            param1.WriteInt(param1.Shift(this.name_99, 10));
            param1.WriteInt(param1.Shift(this.var_2726, 31));
            param1.WriteInt(param1.Shift(this.var_3605, 18));
            param1.WriteInt(param1.Shift(this.var_3932, 20));
            param1.WriteInt(param1.Shift(this.var_2050, 30));
        }
    }
}
