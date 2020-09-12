using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClanChangedCommand : ICommand {

        public short ID { get; set; } = 27824;
        public string clanTag = "";
        public int clanId = 0;
        public int userId = 0;

        public ClanChangedCommand(string param1 = "", int param2 = 0, int param3 = 0) {
            this.clanTag = param1;
            this.clanId = param2;
            this.userId = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.clanTag = param1.ReadUTF();
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 13);
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.clanTag);
            param1.WriteInt(param1.Shift(this.clanId, 19));
            param1.WriteShort(-18356);
            param1.WriteInt(param1.Shift(this.userId, 2));
        }
    }
}
