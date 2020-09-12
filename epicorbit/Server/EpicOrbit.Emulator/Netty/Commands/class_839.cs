using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_839 : ICommand {

        public short ID { get; set; } = 24121;
        public bool replace = false;
        public int var_1567 = 0;
        public int itemId = 0;
        public int slotId = 0;

        public class_839(int param1 = 0, int param2 = 0, int param3 = 0, bool param4 = false) {
            this.var_1567 = param1;
            this.itemId = param2;
            this.slotId = param3;
            this.replace = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.replace = param1.ReadBoolean();
            this.var_1567 = param1.ReadInt();
            this.var_1567 = param1.Shift(this.var_1567, 15);
            this.itemId = param1.ReadInt();
            this.itemId = param1.Shift(this.itemId, 11);
            this.slotId = param1.ReadInt();
            this.slotId = param1.Shift(this.slotId, 21);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.replace);
            param1.WriteInt(param1.Shift(this.var_1567, 17));
            param1.WriteInt(param1.Shift(this.itemId, 21));
            param1.WriteInt(param1.Shift(this.slotId, 11));
            param1.WriteShort(-5240);
            param1.WriteShort(16369);
        }
    }
}
