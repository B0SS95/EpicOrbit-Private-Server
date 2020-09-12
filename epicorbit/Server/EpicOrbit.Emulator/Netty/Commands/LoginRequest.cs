using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LoginRequest : ICommand {

        public short ID { get; set; } = 23263;
        public short factionID = 0;
        public string sessionID = "";
        public int instanceId = 0;
        public int userID = 0;
        public string version = "";

        public LoginRequest(int param1 = 0, short param2 = 0, string param3 = "", string param4 = "", int param5 = 0) {
            this.userID = param1;
            this.factionID = param2;
            this.sessionID = param3;
            this.version = param4;
            this.instanceId = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.factionID = param1.ReadShort();
            param1.ReadShort();
            this.sessionID = param1.ReadUTF();
            this.instanceId = param1.ReadInt();
            this.instanceId = param1.Shift(this.instanceId, 6);
            this.userID = param1.ReadInt();
            this.userID = param1.Shift(this.userID, 16);
            this.version = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.factionID);
            param1.WriteShort(-21392);
            param1.WriteUTF(this.sessionID);
            param1.WriteInt(param1.Shift(this.instanceId, 26));
            param1.WriteInt(param1.Shift(this.userID, 16));
            param1.WriteUTF(this.version);
            param1.WriteShort(-30649);
        }
    }
}
