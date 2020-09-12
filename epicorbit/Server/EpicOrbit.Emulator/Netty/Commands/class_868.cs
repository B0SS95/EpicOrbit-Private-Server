using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_868 : ICommand {

        public short ID { get; set; } = 18552;
        public string lootId = "";
        public int hours = 0;

        public class_868(string param1 = "", int param2 = 0) {
            this.lootId = param1;
            this.hours = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.lootId = param1.ReadUTF();
            this.hours = param1.ReadInt();
            this.hours = param1.Shift(this.hours, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.lootId);
            param1.WriteInt(param1.Shift(this.hours, 20));
        }
    }
}
