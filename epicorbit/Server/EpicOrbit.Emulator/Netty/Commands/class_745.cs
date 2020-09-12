using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_745 : ICommand {

        public short ID { get; set; } = 19655;
        public int maxValue = 0;
        public string var_1887 = "";
        public int currentValue = 0;

        public class_745(string param1 = "", int param2 = 0, int param3 = 0) {
            this.var_1887 = param1;
            this.currentValue = param2;
            this.maxValue = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.maxValue = param1.ReadInt();
            this.maxValue = param1.Shift(this.maxValue, 6);
            this.var_1887 = param1.ReadUTF();
            this.currentValue = param1.ReadInt();
            this.currentValue = param1.Shift(this.currentValue, 7);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.maxValue, 26));
            param1.WriteUTF(this.var_1887);
            param1.WriteInt(param1.Shift(this.currentValue, 25));
        }
    }
}
