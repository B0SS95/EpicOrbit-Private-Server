using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_734 : ICommand {

        public short ID { get; set; } = 24498;
        public string name_176 = "";
        public int rank = 0;

        public class_734(string param1 = "", int param2 = 0) {
            this.name_176 = param1;
            this.rank = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_176 = param1.ReadUTF();
            this.rank = param1.ReadInt();
            this.rank = param1.Shift(this.rank, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.name_176);
            param1.WriteInt(param1.Shift(this.rank, 20));
        }
    }
}
