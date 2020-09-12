using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HuntEventNotificationCommand : ICommand {

        public const short START = 0;
        public const short END = 1;
        public short ID { get; set; } = 11756;
        public short notificationType = 0;
        public int eventId = 0;

        public HuntEventNotificationCommand(short param1 = 0, int param2 = 0) {
            this.notificationType = param1;
            this.eventId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.notificationType = param1.ReadShort();
            this.eventId = param1.ReadInt();
            this.eventId = param1.Shift(this.eventId, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.notificationType);
            param1.WriteInt(param1.Shift(this.eventId, 13));
        }
    }
}
