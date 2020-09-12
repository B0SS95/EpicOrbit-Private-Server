using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_714 : ICommand {

        public short ID { get; set; } = 32512;
        public bool var_2159 = false;
        public string key = "";

        public class_714(string param1 = "", bool param2 = false) {
            this.key = param1;
            this.var_2159 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2159 = param1.ReadBoolean();
            this.key = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-9334);
            param1.WriteBoolean(this.var_2159);
            param1.WriteUTF(this.key);
            param1.WriteShort(-18897);
        }
    }
}
