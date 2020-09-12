using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_956 : ICommand {

        //PetBuffCommand

        public const short const_23 = 1;
        public const short const_688 = 0;
        public short ID { get; set; } = 23207;
        public int var_2247 = 0;
        public List<int> var_4489;
        public short var_4672 = 0;

        public class_956(short param1 = 0, List<int> param2 = null, int param3 = 0) {
            this.var_4672 = param1;
            if (param2 == null) {
                this.var_4489 = new List<int>();
            } else {
                this.var_4489 = param2;
            }
            this.var_2247 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2247 = param1.ReadInt();
            this.var_2247 = param1.Shift(this.var_2247, 14);
            param1.ReadShort();
            this.var_4489.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 1);
                this.var_4489.Add(tmp_0);
            }
            param1.ReadShort();
            this.var_4672 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2247, 18));
            param1.WriteShort(-18812);
            param1.WriteInt(this.var_4489.Count);
            foreach (var tmp_0 in this.var_4489) {
                param1.WriteInt(param1.Shift(tmp_0, 31));
            }
            param1.WriteShort(-2354);
            param1.WriteShort(this.var_4672);
        }
    }
}
