using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class EquipReadyCommand : ICommand {

        public short ID { get; set; } = 11687;
        public bool ready = false;

        public EquipReadyCommand(bool param1 = false) {
            this.ready = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ready = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.ready);
        }
    }
}
