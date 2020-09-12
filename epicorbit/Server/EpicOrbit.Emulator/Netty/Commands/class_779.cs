using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_779 : ICommand {

        public short ID { get; set; } = 26584;
        public int var_1721 = 0;
        public int id = 0;

        public class_779(int param1 = 0, int param2 = 0) {
            this.id = param1;
            this.var_1721 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1721 = param1.ReadInt();
            this.var_1721 = param1.Shift(this.var_1721, 5);
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1721, 27));
            param1.WriteInt(param1.Shift(this.id, 29));
        }
    }
}
