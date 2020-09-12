using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_612 : ICommand {

        public short ID { get; set; } = 6078;
        public int var_4612 = 0;
        public int name_67 = 0;

        public class_612(int param1 = 0, int param2 = 0) {
            this.var_4612 = param1;
            this.name_67 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_4612 = param1.ReadInt();
            this.var_4612 = param1.Shift(this.var_4612, 1);
            this.name_67 = param1.ReadInt();
            this.name_67 = param1.Shift(this.name_67, 13);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(13807);
            param1.WriteInt(param1.Shift(this.var_4612, 31));
            param1.WriteInt(param1.Shift(this.name_67, 19));
            param1.WriteShort(-11394);
        }
    }
}
