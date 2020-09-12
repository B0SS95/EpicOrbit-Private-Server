using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_884 : ICommand {

        public short ID { get; set; } = 18734;
        public List<class_503> updates;

        public class_884(List<class_503> param1 = null) {
            if (param1 == null) {
                this.updates = new List<class_503>();
            } else {
                this.updates = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.updates.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_503;
                tmp_0.Read(param1, lookup);
                this.updates.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.updates.Count);
            foreach (var tmp_0 in this.updates) {
                tmp_0.Write(param1);
            }
        }
    }
}
