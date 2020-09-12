using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SettingSetRequest : ICommand {

        public short ID { get; set; } = 16449;
        public bool immediately = false;
        public string value = "";
        public string key = "";

        public SettingSetRequest(string param1 = "", string param2 = "", bool param3 = false) {
            this.key = param1;
            this.value = param2;
            this.immediately = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.immediately = param1.ReadBoolean();
            this.value = param1.ReadUTF();
            this.key = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.immediately);
            param1.WriteUTF(this.value);
            param1.WriteUTF(this.key);
        }
    }
}
