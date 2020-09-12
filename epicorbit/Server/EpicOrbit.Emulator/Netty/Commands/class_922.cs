using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_922 : MapAddPOICommand, ICommand {

        public override short ID { get; set; } = 12829;
        public bool var_4830 = false;
        public double name_4 = 0;
        public FactionModule name_80;
        public int name_175 = 0;
        public int name_76 = 0;

        public class_922(int param1 = 0, string param2 = "", bool param3 = false, short param4 = 0, bool param5 = false, double param6 = 0, int param7 = 0, POITypeModule param8 = null, FactionModule param9 = null, string param10 = "", List<int> param11 = null, bool param12 = false, POIDesignModule param13 = null)
         : base(param2, param8, param10, param13, param4, param11, param5, param12) {
            if (param9 == null) {
                this.name_80 = new FactionModule();
            } else {
                this.name_80 = param9;
            }
            this.name_175 = param1;
            this.name_76 = param7;
            this.name_4 = param6;
            this.var_4830 = param3;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_4830 = param1.ReadBoolean();
            this.name_4 = param1.ReadDouble();
            this.name_80 = lookup.Lookup(param1) as FactionModule;
            this.name_80.Read(param1, lookup);
            param1.ReadShort();
            this.name_175 = param1.ReadInt();
            this.name_175 = param1.Shift(this.name_175, 10);
            this.name_76 = param1.ReadInt();
            this.name_76 = param1.Shift(this.name_76, 18);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-4414);
            param1.WriteBoolean(this.var_4830);
            param1.WriteDouble(this.name_4);
            this.name_80.Write(param1);
            param1.WriteShort(11044);
            param1.WriteInt(param1.Shift(this.name_175, 22));
            param1.WriteInt(param1.Shift(this.name_76, 14));
        }
    }
}
