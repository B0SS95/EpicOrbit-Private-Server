using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_751 : ICommand {

        public const short const_169 = 6;
        public const short const_916 = 3;
        public const short const_2742 = 2;
        public const short const_679 = 1;
        public const short const_2525 = 4;
        public const short const_1503 = 5;
        public const short const_3266 = 0;
        public short ID { get; set; } = 18497;
        public short var_935 = 0;
        public string userName = "";
        public int userId = 0;

        public class_751(string param1 = "", int param2 = 0, short param3 = 0) {
            this.userName = param1;
            this.userId = param2;
            this.var_935 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_935 = param1.ReadShort();
            this.userName = param1.ReadUTF();
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 28);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_935);
            param1.WriteUTF(this.userName);
            param1.WriteShort(-10258);
            param1.WriteInt(param1.Shift(this.userId, 4));
            param1.WriteShort(-11561);
        }
    }
}
