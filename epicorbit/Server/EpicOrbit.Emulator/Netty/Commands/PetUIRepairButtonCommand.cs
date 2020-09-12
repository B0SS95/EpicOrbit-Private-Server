using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetUIRepairButtonCommand : ICommand {

        public short ID { get; set; } = 28211;
        public bool enabled = false;
        public int repairCosts = 0;

        public PetUIRepairButtonCommand(bool param1 = false, int param2 = 0) {
            this.enabled = param1;
            this.repairCosts = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.enabled = param1.ReadBoolean();
            this.repairCosts = param1.ReadInt();
            this.repairCosts = param1.Shift(this.repairCosts, 14);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.enabled);
            param1.WriteInt(param1.Shift(this.repairCosts, 18));
            param1.WriteShort(20922);
        }
    }
}
