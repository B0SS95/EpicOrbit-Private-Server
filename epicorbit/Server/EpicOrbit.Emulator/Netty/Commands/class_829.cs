using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_829 : ICommand {

        public short ID { get; set; } = 12833;
        public int var_1567 = 0;
        public int slotId = 0;

        public class_829(int param1 = 0, int param2 = 0) {
            this.var_1567 = param1;
            this.slotId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1567 = param1.ReadInt();
            this.var_1567 = param1.Shift(this.var_1567, 20);
            param1.ReadShort();
            this.slotId = param1.ReadInt();
            this.slotId = param1.Shift(this.slotId, 28);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1567, 12));
            param1.WriteShort(-10497);
            param1.WriteInt(param1.Shift(this.slotId, 4));
        }
    }
}
