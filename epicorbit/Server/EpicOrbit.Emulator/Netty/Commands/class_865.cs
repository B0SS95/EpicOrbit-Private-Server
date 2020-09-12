using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_865 : ICommand {

        public short ID { get; set; } = 15603;
        public int var_3378 = 0;

        public class_865(int param1 = 0) {
            this.var_3378 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_3378 = param1.ReadInt();
            this.var_3378 = param1.Shift(this.var_3378, 25);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-7139);
            param1.WriteInt(param1.Shift(this.var_3378, 7));
        }
    }
}
