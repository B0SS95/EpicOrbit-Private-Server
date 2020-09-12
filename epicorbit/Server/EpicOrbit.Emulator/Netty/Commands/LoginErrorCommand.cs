using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LoginErrorCommand : ICommand {

        public const short NO_HITPOINTS_LEFT = 0;
        public const short MAP_NOT_ON_SERVER = 4;
        public const short NO_OLD_CLIENT_ALLOWED = 5;
        public const short INVALID_SESSION_ID = 3;
        public const short NOT_LOGGED_IN_ON_WEBSERVER = 1;
        public const short NO_COMMAND_SESSION_FOUND = 6;
        public const short ALREADY_LOGGED_IN = 2;
        public short ID { get; set; } = 8658;
        public short errorCode = 0;
        public int mapId = 0;

        public LoginErrorCommand(short param1 = 0, int param2 = 0) {
            this.errorCode = param1;
            this.mapId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.errorCode = param1.ReadShort();
            param1.ReadShort();
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.errorCode);
            param1.WriteShort(-29548);
            param1.WriteInt(param1.Shift(this.mapId, 15));
        }
    }
}
