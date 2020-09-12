using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_455 : ICommand {

        public const short const_3372 = 1;
        public const short const_1571 = 0;
        public const short const_421 = 3;
        public const short const_3385 = 2;
        public short ID { get; set; } = 27839;
        public short var_4502 = 0;

        public class_455(short param1 = 0) {
            this.var_4502 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4502 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_4502);
        }
    }
}
