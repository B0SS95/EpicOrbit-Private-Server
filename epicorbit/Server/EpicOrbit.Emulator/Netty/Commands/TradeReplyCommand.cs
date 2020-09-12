using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class TradeReplyCommand : ICommand {

        public short ID { get; set; } = 584;
        public List<OrePriceModule> priceInfos;

        public TradeReplyCommand(List<OrePriceModule> param1 = null) {
            if (param1 == null) {
                this.priceInfos = new List<OrePriceModule>();
            } else {
                this.priceInfos = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.priceInfos.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as OrePriceModule;
                tmp_0.Read(param1, lookup);
                this.priceInfos.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-31499);
            param1.WriteShort(-5599);
            param1.WriteInt(this.priceInfos.Count);
            foreach (var tmp_0 in this.priceInfos) {
                tmp_0.Write(param1);
            }
        }
    }
}
