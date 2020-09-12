using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionPrizeItem : ICommand {

        public const short URIDIUM = 0;
        public const short CREDIT = 1;
        public short ID { get; set; } = 18191;
        public int amount = 0;
        public AmmunitionTypeModule ammunitionType;
        public short currencyType = 0;
        public int summedPrize = 0;

        public AmmunitionPrizeItem(AmmunitionTypeModule param1 = null, short param2 = 0, int param3 = 0, int param4 = 0) {
            if (param1 == null) {
                this.ammunitionType = new AmmunitionTypeModule();
            } else {
                this.ammunitionType = param1;
            }
            this.currencyType = param2;
            this.amount = param3;
            this.summedPrize = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 13);
            this.ammunitionType = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.ammunitionType.Read(param1, lookup);
            this.currencyType = param1.ReadShort();
            this.summedPrize = param1.ReadInt();
            this.summedPrize = param1.Shift(this.summedPrize, 19);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-30657);
            param1.WriteInt(param1.Shift(this.amount, 19));
            this.ammunitionType.Write(param1);
            param1.WriteShort(this.currencyType);
            param1.WriteInt(param1.Shift(this.summedPrize, 13));
            param1.WriteShort(-31611);
        }
    }
}
