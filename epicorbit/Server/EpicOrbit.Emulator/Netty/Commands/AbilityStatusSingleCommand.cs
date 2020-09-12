using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AbilityStatusSingleCommand : ICommand {

        public short ID { get; set; } = 373;
        public int abilityTypeId = 0;
        public bool isActivatable = false;

        public AbilityStatusSingleCommand(int param1 = 0, bool param2 = false) {
            this.abilityTypeId = param1;
            this.isActivatable = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.abilityTypeId = param1.ReadInt();
            this.abilityTypeId = param1.Shift(this.abilityTypeId, 7);
            this.isActivatable = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.abilityTypeId, 25));
            param1.WriteBoolean(this.isActivatable);
            param1.WriteShort(431);
        }
    }
}
