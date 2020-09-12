using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_891 : ICommand {

        public short ID { get; set; } = 18872;
        public string lootId = "";
        public int amount = 0;

        public class_891(string param1 = "", int param2 = 0) {
            this.lootId = param1;
            this.amount = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.lootId = param1.ReadUTF();
            param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 9);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(8535);
            param1.WriteUTF(this.lootId);
            param1.WriteShort(32684);
            param1.WriteInt(param1.Shift(this.amount, 23));
        }
    }
}
