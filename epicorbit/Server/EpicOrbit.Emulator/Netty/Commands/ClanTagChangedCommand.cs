using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClanTagChangedCommand : class_543, ICommand {

        public override short ID { get; set; } = 21849;
        public string clanTag = "";

        public ClanTagChangedCommand(string param1 = "") {
            this.clanTag = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.clanTag = param1.ReadUTF();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteUTF(this.clanTag);
        }
    }
}
