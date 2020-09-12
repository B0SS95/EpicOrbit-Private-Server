using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_896 : ICommand {

        public short ID { get; set; } = 18038;
        public string name = "";
        public List<class_896> subAttributes;
        public class_540 value;

        public class_896(List<class_896> param1 = null, string param2 = "", class_540 param3 = null) {
            if (param1 == null) {
                this.subAttributes = new List<class_896>();
            } else {
                this.subAttributes = param1;
            }
            this.name = param2;
            if (param3 == null) {
                this.value = new class_540();
            } else {
                this.value = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name = param1.ReadUTF();
            this.subAttributes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_896;
                tmp_0.Read(param1, lookup);
                this.subAttributes.Add(tmp_0);
            }
            param1.ReadShort();
            this.value = lookup.Lookup(param1) as class_540;
            this.value.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.name);
            param1.WriteInt(this.subAttributes.Count);
            foreach (var tmp_0 in this.subAttributes) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-15033);
            this.value.Write(param1);
            param1.WriteShort(29107);
        }
    }
}
