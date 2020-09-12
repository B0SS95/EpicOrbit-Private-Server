using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetGearRemoveCommand : ICommand {

        public short ID { get; set; } = 24735;
        public PetGearTypeModule gearType;
        public int level = 0;
        public int amount = 0;

        public PetGearRemoveCommand(PetGearTypeModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.gearType = new PetGearTypeModule();
            } else {
                this.gearType = param1;
            }
            this.level = param2;
            this.amount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.gearType = lookup.Lookup(param1) as PetGearTypeModule;
            this.gearType.Read(param1, lookup);
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 6);
            param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 15);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.gearType.Write(param1);
            param1.WriteInt(param1.Shift(this.level, 26));
            param1.WriteShort(-31536);
            param1.WriteInt(param1.Shift(this.amount, 17));
        }
    }
}
