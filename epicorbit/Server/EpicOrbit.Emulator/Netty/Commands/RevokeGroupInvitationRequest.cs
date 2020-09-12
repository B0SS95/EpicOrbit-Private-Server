using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class RevokeGroupInvitationRequest : ICommand {

        public short ID { get; set; } = 24931;
        public int playerId = 0;

        public RevokeGroupInvitationRequest(int param1 = 0) {
            this.playerId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.playerId = param1.ReadInt();
            this.playerId = param1.Shift(this.playerId, 21);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-25145);
            param1.WriteInt(param1.Shift(this.playerId, 11));
        }
    }
}
