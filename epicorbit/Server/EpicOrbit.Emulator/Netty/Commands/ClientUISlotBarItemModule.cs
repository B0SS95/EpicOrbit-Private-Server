using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarItemModule : ICommand {

        public short ID { get; set; } = 17494;
        public int slotId = 0;
        public string var_2176 = "";

        public ClientUISlotBarItemModule(int param1 = 0, string param2 = "") {
            this.slotId = param1;
            this.var_2176 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.slotId = param1.ReadInt();
            this.slotId = param1.Shift(this.slotId, 28);
            this.var_2176 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.slotId, 4));
            param1.WriteUTF(this.var_2176);
        }
    }
}
