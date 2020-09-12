using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionBuyCommand : ICommand {

        public const short SUCCESSFULL = 0;
        public const short NOT_ENOUGH_CREDITS = 1;
        public const short NOT_ENOUGH_URIDIUM = 2;
        public const short NOT_ENOUGH_SPACE = 3;
        public short ID { get; set; } = 833;
        public AmmunitionTypeModule ammunitionType;
        public short transactionStatus = 0;
        public int amount = 0;

        public AmmunitionBuyCommand(short param1 = 0, AmmunitionTypeModule param2 = null, int param3 = 0) {
            this.transactionStatus = param1;
            if (param2 == null) {
                this.ammunitionType = new AmmunitionTypeModule();
            } else {
                this.ammunitionType = param2;
            }
            this.amount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ammunitionType = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.ammunitionType.Read(param1, lookup);
            this.transactionStatus = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 24);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.ammunitionType.Write(param1);
            param1.WriteShort(this.transactionStatus);
            param1.WriteShort(-17810);
            param1.WriteShort(26879);
            param1.WriteInt(param1.Shift(this.amount, 8));
        }
    }
}
