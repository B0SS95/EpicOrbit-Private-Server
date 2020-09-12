using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_860 : ICommand {

        public short ID { get; set; } = 18476;
        public int userId = 0;
        public string name = "";
        public bool active = false;

        public class_860(int param1 = 0, string param2 = "", bool param3 = false) {
            this.userId = param1;
            this.name = param2;
            this.active = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 25);
            this.name = param1.ReadUTF();
            this.active = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.userId, 7));
            param1.WriteUTF(this.name);
            param1.WriteBoolean(this.active);
            param1.WriteShort(-25830);
        }
    }
}
