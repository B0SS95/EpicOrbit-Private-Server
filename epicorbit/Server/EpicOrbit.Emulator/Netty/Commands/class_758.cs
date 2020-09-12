using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_758 : ICommand {

        public short ID { get; set; } = 24841;
        public bool var_979 = false;
        public string var_3566 = "";

        public class_758(string param1 = "", bool param2 = false) {
            this.var_3566 = param1;
            this.var_979 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_979 = param1.ReadBoolean();
            this.var_3566 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_979);
            param1.WriteUTF(this.var_3566);
        }
    }
}
