using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_694 : ICommand {

        public short ID { get; set; } = 14437;
        public List<int> var_999;

        public class_694(List<int> param1 = null) {
            if (param1 == null) {
                this.var_999 = new List<int>();
            } else {
                this.var_999 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_999.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 25);
                this.var_999.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(11146);
            param1.WriteInt(this.var_999.Count);
            foreach (var tmp_0 in this.var_999) {
                param1.WriteInt(param1.Shift(tmp_0, 7));
            }
        }
    }
}
