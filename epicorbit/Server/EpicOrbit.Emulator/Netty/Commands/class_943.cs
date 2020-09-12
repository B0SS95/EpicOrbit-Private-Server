using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_943 : ICommand {

        public short ID { get; set; } = 21847;
        public int var_207 = 0;
        public string id = "";
        public int progress = 0;

        public class_943(string param1 = "", int param2 = 0, int param3 = 0) {
            this.id = param1;
            this.progress = param2;
            this.var_207 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_207 = param1.ReadInt();
            this.var_207 = param1.Shift(this.var_207, 26);
            this.id = param1.ReadUTF();
            this.progress = param1.ReadInt();
            this.progress = param1.Shift(this.progress, 7);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_207, 6));
            param1.WriteUTF(this.id);
            param1.WriteInt(param1.Shift(this.progress, 25));
        }
    }
}
