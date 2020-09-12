using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_286 : ICommand {

        public short ID { get; set; } = 12984;
        public int var_3213 = 0;
        public int var_2726 = 0;
        public int var_361 = 0;
        public int var_2226 = 0;
        public FactionModule var_3191;
        public int name_99 = 0;
        public int var_3605 = 0;
        public int var_4027 = 0;
        public int var_2050 = 0;
        public FactionModule var_4496;
        public int var_355 = 0;

        public class_286(int param1 = 0, FactionModule param2 = null, FactionModule param3 = null, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, int param11 = 0) {
            this.name_99 = param1;
            if (param2 == null) {
                this.var_4496 = new FactionModule();
            } else {
                this.var_4496 = param2;
            }
            if (param3 == null) {
                this.var_3191 = new FactionModule();
            } else {
                this.var_3191 = param3;
            }
            this.var_3605 = param4;
            this.var_4027 = param5;
            this.var_2726 = param6;
            this.var_2050 = param7;
            this.var_3213 = param8;
            this.var_355 = param9;
            this.var_2226 = param10;
            this.var_361 = param11;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3213 = param1.ReadInt();
            this.var_3213 = param1.Shift(this.var_3213, 2);
            this.var_2726 = param1.ReadInt();
            this.var_2726 = param1.Shift(this.var_2726, 15);
            this.var_361 = param1.ReadInt();
            this.var_361 = param1.Shift(this.var_361, 26);
            this.var_2226 = param1.ReadInt();
            this.var_2226 = param1.Shift(this.var_2226, 25);
            this.var_3191 = lookup.Lookup(param1) as FactionModule;
            this.var_3191.Read(param1, lookup);
            this.name_99 = param1.ReadInt();
            this.name_99 = param1.Shift(this.name_99, 23);
            this.var_3605 = param1.ReadInt();
            this.var_3605 = param1.Shift(this.var_3605, 1);
            this.var_4027 = param1.ReadInt();
            this.var_4027 = param1.Shift(this.var_4027, 22);
            param1.ReadShort();
            this.var_2050 = param1.ReadInt();
            this.var_2050 = param1.Shift(this.var_2050, 18);
            this.var_4496 = lookup.Lookup(param1) as FactionModule;
            this.var_4496.Read(param1, lookup);
            this.var_355 = param1.ReadInt();
            this.var_355 = param1.Shift(this.var_355, 28);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_3213, 30));
            param1.WriteInt(param1.Shift(this.var_2726, 17));
            param1.WriteInt(param1.Shift(this.var_361, 6));
            param1.WriteInt(param1.Shift(this.var_2226, 7));
            this.var_3191.Write(param1);
            param1.WriteInt(param1.Shift(this.name_99, 9));
            param1.WriteInt(param1.Shift(this.var_3605, 31));
            param1.WriteInt(param1.Shift(this.var_4027, 10));
            param1.WriteShort(-25890);
            param1.WriteInt(param1.Shift(this.var_2050, 14));
            this.var_4496.Write(param1);
            param1.WriteInt(param1.Shift(this.var_355, 4));
        }
    }
}
