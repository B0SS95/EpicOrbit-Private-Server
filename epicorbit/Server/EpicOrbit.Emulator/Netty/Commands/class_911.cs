using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_911 : ICommand {

        public const short const_728 = 0;
        public const short const_3445 = 1;
        public short ID { get; set; } = 3094;
        public List<class_617> var_1035;
        public class_601 var_1936;
        public short rewardType = 0;

        public class_911(List<class_617> param1 = null, class_601 param2 = null, short param3 = 0) {
            if (param1 == null) {
                this.var_1035 = new List<class_617>();
            } else {
                this.var_1035 = param1;
            }
            if (param2 == null) {
                this.var_1936 = new class_601();
            } else {
                this.var_1936 = param2;
            }
            this.rewardType = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1035.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_617;
                tmp_0.Read(param1, lookup);
                this.var_1035.Add(tmp_0);
            }
            this.var_1936 = lookup.Lookup(param1) as class_601;
            this.var_1936.Read(param1, lookup);
            this.rewardType = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_1035.Count);
            foreach (var tmp_0 in this.var_1035) {
                tmp_0.Write(param1);
            }
            this.var_1936.Write(param1);
            param1.WriteShort(this.rewardType);
            param1.WriteShort(12786);
        }
    }
}
