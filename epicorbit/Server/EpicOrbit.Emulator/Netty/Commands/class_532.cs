using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_532 : ICommand {

        public short ID { get; set; } = 22082;
        public List<class_896> attributes;
        public string name = "";

        public class_532(string param1 = "", List<class_896> param2 = null) {
            this.name = param1;
            if (param2 == null) {
                this.attributes = new List<class_896>();
            } else {
                this.attributes = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.attributes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_896;
                tmp_0.Read(param1, lookup);
                this.attributes.Add(tmp_0);
            }
            this.name = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-3075);
            param1.WriteShort(28943);
            param1.WriteInt(this.attributes.Count);
            foreach (var tmp_0 in this.attributes) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.name);
        }
    }
}
