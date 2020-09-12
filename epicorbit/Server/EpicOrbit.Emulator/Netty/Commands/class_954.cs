using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_954 : ICommand {

        // CollectionBeamStopCommand
        // PetUIRepairButtonCommand

        public short ID { get; set; } = 9003;
        public bool name_98 = false;
        public int name_142 = 0;

        public class_954(bool param1 = false, int param2 = 0) {
            this.name_98 = param1;
            this.name_142 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_98 = param1.ReadBoolean();
            param1.ReadShort();
            this.name_142 = param1.ReadInt();
            this.name_142 = param1.Shift(this.name_142, 4);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.name_98);
            param1.WriteShort(9628);
            param1.WriteInt(param1.Shift(this.name_142, 28));
        }
    }
}
