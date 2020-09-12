using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_986 : class_748, ICommand {

        public override short ID { get; set; } = 3589;
        public List<class_540> var_3800;

        public class_986(List<class_540> param1 = null) {
            if (param1 == null) {
                this.var_3800 = new List<class_540>();
            } else {
                this.var_3800 = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_3800.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_540;
                tmp_0.Read(param1, lookup);
                this.var_3800.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-12738);
            param1.WriteInt(this.var_3800.Count);
            foreach (var tmp_0 in this.var_3800) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-12654);
        }
    }
}
