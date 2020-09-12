using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_598 : ICommand {

        public short ID { get; set; } = 7355;
        public int minLevel = 0;
        public List<class_292> var_2983;
        public int var_1452 = 0;
        public int name_126 = 0;

        public class_598(int param1 = 0, int param2 = 0, int param3 = 0, List<class_292> param4 = null) {
            this.minLevel = param1;
            this.name_126 = param2;
            this.var_1452 = param3;
            if (param4 == null) {
                this.var_2983 = new List<class_292>();
            } else {
                this.var_2983 = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.minLevel = param1.ReadInt();
            this.minLevel = param1.Shift(this.minLevel, 28);
            this.var_2983.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_292;
                tmp_0.Read(param1, lookup);
                this.var_2983.Add(tmp_0);
            }
            this.var_1452 = param1.ReadInt();
            this.var_1452 = param1.Shift(this.var_1452, 27);
            this.name_126 = param1.ReadInt();
            this.name_126 = param1.Shift(this.name_126, 11);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.minLevel, 4));
            param1.WriteInt(this.var_2983.Count);
            foreach (var tmp_0 in this.var_2983) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.var_1452, 5));
            param1.WriteInt(param1.Shift(this.name_126, 21));
            param1.WriteShort(24055);
        }
    }
}
