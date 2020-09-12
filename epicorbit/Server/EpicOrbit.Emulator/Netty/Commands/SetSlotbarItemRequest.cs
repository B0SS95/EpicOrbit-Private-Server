using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SetSlotbarItemRequest : ICommand {

        public short ID { get; set; } = 1599;
        public int toIndex = 0;
        public string fromSlotbarId = "";
        public string itemId = "";
        public int fromIndex = 0;
        public string toSlotbarId = "";

        public SetSlotbarItemRequest(string param1 = "", int param2 = 0, string param3 = "", int param4 = 0, string param5 = "") {
            this.fromSlotbarId = param1;
            this.fromIndex = param2;
            this.toSlotbarId = param3;
            this.toIndex = param4;
            this.itemId = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.toIndex = param1.ReadInt();
            this.toIndex = param1.Shift(this.toIndex, 4);
            this.fromSlotbarId = param1.ReadUTF();
            param1.ReadShort();
            this.itemId = param1.ReadUTF();
            this.fromIndex = param1.ReadInt();
            this.fromIndex = param1.Shift(this.fromIndex, 21);
            this.toSlotbarId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.toIndex, 28));
            param1.WriteUTF(this.fromSlotbarId);
            param1.WriteShort(26664);
            param1.WriteUTF(this.itemId);
            param1.WriteInt(param1.Shift(this.fromIndex, 11));
            param1.WriteUTF(this.toSlotbarId);
        }
    }
}
