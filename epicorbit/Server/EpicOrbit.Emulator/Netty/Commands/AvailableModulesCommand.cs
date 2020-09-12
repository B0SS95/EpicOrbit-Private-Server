using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AvailableModulesCommand : ICommand {

        public short ID { get; set; } = 21231;
        public List<StationModuleModule> modules;

        public AvailableModulesCommand(List<StationModuleModule> param1 = null) {
            if (param1 == null) {
                this.modules = new List<StationModuleModule>();
            } else {
                this.modules = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.modules.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as StationModuleModule;
                tmp_0.Read(param1, lookup);
                this.modules.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.modules.Count);
            foreach (var tmp_0 in this.modules) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-26936);
        }
    }
}
