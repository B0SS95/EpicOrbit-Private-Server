using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_585 : ICommand {

        public short ID { get; set; } = 8989;
        public string var_4252 = "";
        public int userId = 0;

        public class_585(int param1 = 0, string param2 = "") {
            this.userId = param1;
            this.var_4252 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_4252 = param1.ReadUTF();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-2883);
            param1.WriteUTF(this.var_4252);
            param1.WriteInt(param1.Shift(this.userId, 29));
        }
    }
}
