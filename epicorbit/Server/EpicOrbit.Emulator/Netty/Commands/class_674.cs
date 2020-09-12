using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_674 : ICommand {

        public short ID { get; set; } = 10333;
        public List<class_734> name_63;

        public class_674(List<class_734> param1 = null) {
            if (param1 == null) {
                this.name_63 = new List<class_734>();
            } else {
                this.name_63 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.name_63.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_734;
                tmp_0.Read(param1, lookup);
                this.name_63.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-15397);
            param1.WriteInt(this.name_63.Count);
            foreach (var tmp_0 in this.name_63) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-4542);
        }
    }
}
