using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeOreCountUpdateCommand : ICommand {

        public short ID { get; set; } = 22221;
        public List<OreCountModule> oreCountList;

        public AttributeOreCountUpdateCommand(List<OreCountModule> param1 = null) {
            if (param1 == null) {
                this.oreCountList = new List<OreCountModule>();
            } else {
                this.oreCountList = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.oreCountList.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as OreCountModule;
                tmp_0.Read(param1, lookup);
                this.oreCountList.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.oreCountList.Count);
            foreach (var tmp_0 in this.oreCountList) {
                tmp_0.Write(param1);
            }
        }
    }
}
