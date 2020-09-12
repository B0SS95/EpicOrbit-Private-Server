using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_707 : ICommand {

        public short ID { get; set; } = 31362;
        public List<class_1004> var_2199;

        public class_707(List<class_1004> param1 = null) {
            if (param1 == null) {
                this.var_2199 = new List<class_1004>();
            } else {
                this.var_2199 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2199.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_1004;
                tmp_0.Read(param1, lookup);
                this.var_2199.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_2199.Count);
            foreach (var tmp_0 in this.var_2199) {
                tmp_0.Write(param1);
            }
        }
    }
}
