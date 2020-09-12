using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_827 : ICommand {

        public short ID { get; set; } = 4976;
        public string range = "";
        public List<class_617> items;

        public class_827(string param1 = "", List<class_617> param2 = null) {
            this.range = param1;
            if (param2 == null) {
                this.items = new List<class_617>();
            } else {
                this.items = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.range = param1.ReadUTF();
            this.items.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_617;
                tmp_0.Read(param1, lookup);
                this.items.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(19816);
            param1.WriteUTF(this.range);
            param1.WriteInt(this.items.Count);
            foreach (var tmp_0 in this.items) {
                tmp_0.Write(param1);
            }
        }
    }
}
