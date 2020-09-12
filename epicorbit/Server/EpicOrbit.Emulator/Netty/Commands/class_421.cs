using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_421 : ICommand {

        public const short const_267 = 4;
        public const short const_2711 = 6;
        public const short const_1192 = 0;
        public const short const_459 = 1;
        public const short const_1361 = 2;
        public const short const_683 = 7;
        public const short const_696 = 3;
        public const short const_1884 = 5;
        public short ID { get; set; } = 28084;
        public short var_2249 = 0;

        public class_421(short param1 = 0) {
            this.var_2249 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2249 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(2748);
            param1.WriteShort(this.var_2249);
        }
    }
}
