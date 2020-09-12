using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_760 : ICommand {

        public short ID { get; set; } = 14205;
        public bool var_4583 = false;
        public string name_15 = "";

        public class_760(string param1 = "", bool param2 = false) {
            this.name_15 = param1;
            this.var_4583 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4583 = param1.ReadBoolean();
            param1.ReadShort();
            this.name_15 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_4583);
            param1.WriteShort(-10541);
            param1.WriteUTF(this.name_15);
        }
    }
}
