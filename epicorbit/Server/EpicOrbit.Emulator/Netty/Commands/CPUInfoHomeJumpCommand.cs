using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CPUInfoHomeJumpCommand : ICommand {

        public short ID { get; set; } = 29378;
        public int chargesLeft = 0;
        public int level = 0;
        public int homeMapId = 0;

        public CPUInfoHomeJumpCommand(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.level = param1;
            this.homeMapId = param2;
            this.chargesLeft = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.chargesLeft = param1.ReadInt();
            this.chargesLeft = param1.Shift(this.chargesLeft, 30);
            param1.ReadShort();
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 19);
            this.homeMapId = param1.ReadInt();
            this.homeMapId = param1.Shift(this.homeMapId, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.chargesLeft, 2));
            param1.WriteShort(15804);
            param1.WriteInt(param1.Shift(this.level, 13));
            param1.WriteInt(param1.Shift(this.homeMapId, 15));
        }
    }
}
