using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_630 : ICommand {

        // ClanMemberJoinedCommand
        // ClanMemberModule

        public short ID { get; set; } = 24292;
        public string userName = "";
        public int userId = 0;

        public class_630(int param1 = 0, string param2 = "") {
            this.userId = param1;
            this.userName = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userName = param1.ReadUTF();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 24);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.userName);
            param1.WriteInt(param1.Shift(this.userId, 8));
        }
    }
}
