using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionPrizeInfoCommand : ICommand {

        public short ID { get; set; } = 21798;
        public List<AmmunitionPrizeItem> ammunitionPrizeItemList;

        public AmmunitionPrizeInfoCommand(List<AmmunitionPrizeItem> param1 = null) {
            if (param1 == null) {
                this.ammunitionPrizeItemList = new List<AmmunitionPrizeItem>();
            } else {
                this.ammunitionPrizeItemList = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ammunitionPrizeItemList.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as AmmunitionPrizeItem;
                tmp_0.Read(param1, lookup);
                this.ammunitionPrizeItemList.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.ammunitionPrizeItemList.Count);
            foreach (var tmp_0 in this.ammunitionPrizeItemList) {
                tmp_0.Write(param1);
            }
        }
    }
}
