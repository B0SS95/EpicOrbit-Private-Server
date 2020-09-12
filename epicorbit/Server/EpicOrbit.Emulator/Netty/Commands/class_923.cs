using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_923 : ICommand {

        public short ID { get; set; } = 5281;
        public List<class_775> windows;

        public class_923(List<class_775> param1 = null) {
            if (param1 == null) {
                this.windows = new List<class_775>();
            } else {
                this.windows = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.windows.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_775;
                tmp_0.Read(param1, lookup);
                this.windows.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-9569);
            param1.WriteInt(this.windows.Count);
            foreach (var tmp_0 in this.windows) {
                tmp_0.Write(param1);
            }
        }
    }
}
