using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_626 : ICommand {

        public const short TECH_FACTORY = 0;
        public short ID { get; set; } = 31072;
        public bool name_93 = false;
        public short var_637 = 0;

        public class_626(short param1 = 0, bool param2 = false) {
            this.var_637 = param1;
            this.name_93 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_93 = param1.ReadBoolean();
            param1.ReadShort();
            this.var_637 = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.name_93);
            param1.WriteShort(31089);
            param1.WriteShort(this.var_637);
            param1.WriteShort(8556);
        }
    }
}
