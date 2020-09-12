using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1065 : ICommand {

        public short ID { get; set; } = 25990;
        public int var_1574 = 0;
        public string var_3286 = "";

        public class_1065(int param1 = 0, string param2 = "") {
            this.var_1574 = param1;
            this.var_3286 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_1574 = param1.ReadInt();
            this.var_1574 = param1.Shift(this.var_1574, 20);
            this.var_3286 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(6328);
            param1.WriteInt(param1.Shift(this.var_1574, 12));
            param1.WriteUTF(this.var_3286);
        }
    }
}
