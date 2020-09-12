using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowActionCommand : ICommand {

        public short ID { get; set; } = 977;
        public List<UIWindowActionModule> windowActions;

        public UIWindowActionCommand(List<UIWindowActionModule> param1 = null) {
            if (param1 == null) {
                this.windowActions = new List<UIWindowActionModule>();
            } else {
                this.windowActions = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.windowActions.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as UIWindowActionModule;
                tmp_0.Read(param1, lookup);
                this.windowActions.Add(tmp_0);
            }
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.windowActions.Count);
            foreach (var tmp_0 in this.windowActions) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(28318);
            param1.WriteShort(-30769);
        }
    }
}
