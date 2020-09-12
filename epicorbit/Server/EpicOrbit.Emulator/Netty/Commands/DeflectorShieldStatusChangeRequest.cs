using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DeflectorShieldStatusChangeRequest : ICommand {

        public short ID { get; set; } = 24015;
        public int battleStationId = 0;
        public int minutes = 0;

        public DeflectorShieldStatusChangeRequest(int param1 = 0, int param2 = 0) {
            this.battleStationId = param1;
            this.minutes = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 16);
            param1.ReadShort();
            this.minutes = param1.ReadInt();
            this.minutes = param1.Shift(this.minutes, 25);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.battleStationId, 16));
            param1.WriteShort(-1613);
            param1.WriteInt(param1.Shift(this.minutes, 7));
        }
    }
}
