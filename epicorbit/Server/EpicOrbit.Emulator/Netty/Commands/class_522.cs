using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_522 : ICommand {

        public short ID { get; set; } = 13718;
        public List<class_649> var_5041;

        public class_522(List<class_649> param1 = null) {
            if (param1 == null) {
                this.var_5041 = new List<class_649>();
            } else {
                this.var_5041 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5041.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_649;
                tmp_0.Read(param1, lookup);
                this.var_5041.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_5041.Count);
            foreach (var tmp_0 in this.var_5041) {
                tmp_0.Write(param1);
            }
        }
    }
}
