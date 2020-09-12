using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_908 : class_503, ICommand {

        public override short ID { get; set; } = 10968;
        public int name_46 = 0;
        public string clanTag = "";

        public class_908(string param1 = "", int param2 = 0) {
            this.name_46 = param2;
            this.clanTag = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.name_46 = param1.ReadInt();
            this.name_46 = param1.Shift(this.name_46, 10);
            this.clanTag = param1.ReadUTF();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.name_46, 22));
            param1.WriteUTF(this.clanTag);
        }
    }
}
