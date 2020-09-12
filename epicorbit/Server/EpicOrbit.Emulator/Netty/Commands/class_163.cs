using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_163 : ICommand {

        public const short const_3137 = 1;
        public const short const_142 = 2;
        public const short const_3404 = 0;
        public short ID { get; set; } = 11146;
        public short var_1434 = 0;
        public string var_4210 = "";

        public class_163(string param1 = "", short param2 = 0) {
            this.var_4210 = param1;
            this.var_1434 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1434 = param1.ReadShort();
            this.var_4210 = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_1434);
            param1.WriteUTF(this.var_4210);
            param1.WriteShort(3526);
        }
    }
}
