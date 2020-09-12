using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUITooltipsCommand : ICommand {

        public short ID { get; set; } = 29480;
        public List<ClientUITooltipModule> tooltips;

        public ClientUITooltipsCommand(List<ClientUITooltipModule> param1 = null) {
            if (param1 == null) {
                this.tooltips = new List<ClientUITooltipModule>();
            } else {
                this.tooltips = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.tooltips.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUITooltipModule;
                tmp_0.Read(param1, lookup);
                this.tooltips.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.tooltips.Count);
            foreach (var tmp_0 in this.tooltips) {
                tmp_0.Write(param1);
            }
        }
    }
}
