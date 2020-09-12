using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class RejectGroupInvitationRequest : ICommand {

        public short ID { get; set; } = 2787;
        public int playerId = 0;

        public RejectGroupInvitationRequest(int param1 = 0) {
            this.playerId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playerId = param1.ReadInt();
            this.playerId = param1.Shift(this.playerId, 2);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.playerId, 30));
            param1.WriteShort(32152);
        }
    }
}
