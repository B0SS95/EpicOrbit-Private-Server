using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetGearAddCommand : ICommand {

        public short ID { get; set; } = 4421;
        public bool enabled = false;
        public int amount = 0;
        public PetGearTypeModule gearType;
        public int level = 0;

        public PetGearAddCommand(PetGearTypeModule param1 = null, int param2 = 0, int param3 = 0, bool param4 = false) {
            if (param1 == null) {
                this.gearType = new PetGearTypeModule();
            } else {
                this.gearType = param1;
            }
            this.level = param2;
            this.amount = param3;
            this.enabled = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.enabled = param1.ReadBoolean();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 20);
            this.gearType = lookup.Lookup(param1) as PetGearTypeModule;
            this.gearType.Read(param1, lookup);
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5357);
            param1.WriteShort(-18649);
            param1.WriteBoolean(this.enabled);
            param1.WriteInt(param1.Shift(this.amount, 12));
            this.gearType.Write(param1);
            param1.WriteInt(param1.Shift(this.level, 18));
        }
    }
}
