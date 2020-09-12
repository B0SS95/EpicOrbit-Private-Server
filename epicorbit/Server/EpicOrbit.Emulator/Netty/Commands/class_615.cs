using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_615 : ICommand {

        public short ID { get; set; } = 10962;
        public string name = "";
        public class_748 range;
        public string toolTip = "";
        public List<class_615> subAttributes;
        public bool var_1797 = false;

        public class_615(bool param1 = false, List<class_615> param2 = null, string param3 = "", class_748 param4 = null, string param5 = "") {
            this.var_1797 = param1;
            if (param2 == null) {
                this.subAttributes = new List<class_615>();
            } else {
                this.subAttributes = param2;
            }
            this.name = param3;
            if (param4 == null) {
                this.range = new class_748();
            } else {
                this.range = param4;
            }
            this.toolTip = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name = param1.ReadUTF();
            this.range = lookup.Lookup(param1) as class_748;
            this.range.Read(param1, lookup);
            this.toolTip = param1.ReadUTF();
            this.subAttributes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_615;
                tmp_0.Read(param1, lookup);
                this.subAttributes.Add(tmp_0);
            }
            this.var_1797 = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.name);
            this.range.Write(param1);
            param1.WriteUTF(this.toolTip);
            param1.WriteInt(this.subAttributes.Count);
            foreach (var tmp_0 in this.subAttributes) {
                tmp_0.Write(param1);
            }
            param1.WriteBoolean(this.var_1797);
            param1.WriteShort(-5088);
        }
    }
}
