using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlBonusCommand : ICommand {

        public short ID { get; set; } = 11776;
        public float bonusFactor = 0;
        public BoostedAttributeTypeModule bonusType;
        public FactionModule faction;

        public SectorControlBonusCommand(FactionModule param1 = null, BoostedAttributeTypeModule param2 = null, float param3 = 0) {
            if (param1 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param1;
            }
            if (param2 == null) {
                this.bonusType = new BoostedAttributeTypeModule();
            } else {
                this.bonusType = param2;
            }
            this.bonusFactor = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.bonusFactor = param1.ReadFloat();
            this.bonusType = lookup.Lookup(param1) as BoostedAttributeTypeModule;
            this.bonusType.Read(param1, lookup);
            param1.ReadShort();
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-27660);
            param1.WriteFloat(this.bonusFactor);
            this.bonusType.Write(param1);
            param1.WriteShort(-12757);
            this.faction.Write(param1);
        }
    }
}
