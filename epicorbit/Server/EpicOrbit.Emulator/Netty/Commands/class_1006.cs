using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1006 : ICommand {

        public short ID { get; set; } = 7314;
        public List<class_601> var_25;
        public string name_131 = "";
        public double name_7 = 0;
        public int playerPoints = 0;
        public int totalClanPoints = 0;
        public int var_2247 = 0;
        public List<class_601> var_3344;

        public class_1006(int param1 = 0, double param2 = 0, int param3 = 0, int param4 = 0, string param5 = "", List<class_601> param6 = null, List<class_601> param7 = null) {
            this.var_2247 = param1;
            this.name_7 = param2;
            this.playerPoints = param3;
            this.totalClanPoints = param4;
            this.name_131 = param5;
            if (param6 == null) {
                this.var_25 = new List<class_601>();
            } else {
                this.var_25 = param6;
            }
            if (param7 == null) {
                this.var_3344 = new List<class_601>();
            } else {
                this.var_3344 = param7;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_25.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_601;
                tmp_0.Read(param1, lookup);
                this.var_25.Add(tmp_0);
            }
            param1.ReadShort();
            this.name_131 = param1.ReadUTF();
            this.name_7 = param1.ReadDouble();
            param1.ReadShort();
            this.playerPoints = param1.ReadInt();
            this.playerPoints = param1.Shift(this.playerPoints, 21);
            this.totalClanPoints = param1.ReadInt();
            this.totalClanPoints = param1.Shift(this.totalClanPoints, 20);
            this.var_2247 = param1.ReadInt();
            this.var_2247 = param1.Shift(this.var_2247, 9);
            this.var_3344.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_601;
                tmp_0.Read(param1, lookup);
                this.var_3344.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_25.Count);
            foreach (var tmp_0 in this.var_25) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-17985);
            param1.WriteUTF(this.name_131);
            param1.WriteDouble(this.name_7);
            param1.WriteShort(-26588);
            param1.WriteInt(param1.Shift(this.playerPoints, 11));
            param1.WriteInt(param1.Shift(this.totalClanPoints, 12));
            param1.WriteInt(param1.Shift(this.var_2247, 23));
            param1.WriteInt(this.var_3344.Count);
            foreach (var tmp_0 in this.var_3344) {
                tmp_0.Write(param1);
            }
        }
    }
}
