using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIActionCommand : ICommand {

        public short ID { get; set; } = 537;
        public List<UIButtonActionModule> buttonActions;

        public UIActionCommand(List<UIButtonActionModule> param1 = null) {
            if (param1 == null) {
                this.buttonActions = new List<UIButtonActionModule>();
            } else {
                this.buttonActions = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.buttonActions.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as UIButtonActionModule;
                tmp_0.Read(param1, lookup);
                this.buttonActions.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.buttonActions.Count);
            foreach (var tmp_0 in this.buttonActions) {
                tmp_0.Write(param1);
            }
        }
    }
}
