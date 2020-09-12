using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_881 : ICommand {

        public const short const_1581 = 3;
        public const short const_2780 = 4;
        public const short const_2887 = 7;
        public const short const_1510 = 1;
        public const short const_1957 = 0;
        public const short const_3120 = 6;
        public const short const_3205 = 5;
        public const short const_3309 = 2;
        public short ID { get; set; } = 17642;
        public short var_5118 = 0;

        public class_881(short param1 = 0) {
            this.var_5118 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5118 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_5118);
        }
    }
}
