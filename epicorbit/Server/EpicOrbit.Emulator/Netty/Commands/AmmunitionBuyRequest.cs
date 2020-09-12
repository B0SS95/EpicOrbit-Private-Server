using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionBuyRequest : ICommand {

        public short ID { get; set; } = 23561;
        public int amount = 0;
        public AmmunitionTypeModule amuntionType;

        public AmmunitionBuyRequest(AmmunitionTypeModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.amuntionType = new AmmunitionTypeModule();
            } else {
                this.amuntionType = param1;
            }
            this.amount = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 28);
            this.amuntionType = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.amuntionType.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-29888);
            param1.WriteInt(param1.Shift(this.amount, 4));
            this.amuntionType.Write(param1);
        }
    }
}
