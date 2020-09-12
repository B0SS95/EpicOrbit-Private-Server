using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_693 : ICommand {

        public short ID { get; set; } = 32142;
        public List<class_1074> var_333;
        public string var_2469 = "";

        public class_693(string param1 = "", List<class_1074> param2 = null) {
            this.var_2469 = param1;
            if (param2 == null) {
                this.var_333 = new List<class_1074>();
            } else {
                this.var_333 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_333.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_1074;
                tmp_0.Read(param1, lookup);
                this.var_333.Add(tmp_0);
            }
            this.var_2469 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_333.Count);
            foreach (var tmp_0 in this.var_333) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.var_2469);
        }
    }
}
