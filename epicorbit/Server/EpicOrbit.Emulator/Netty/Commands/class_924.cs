using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_924 : ICommand {

        public const short const_2853 = 1;
        public const short const_600 = 0;
        public const short const_107 = 3;
        public const short const_968 = 2;
        public const short const_525 = 1;
        public const short const_3463 = 0;
        public short ID { get; set; } = 12948;
        public string titleKey = "";
        public int userId = 0;
        public short var_3034 = 0;
        public short type = 0;

        public class_924(short param1 = 0, short param2 = 0, int param3 = 0, string param4 = "") {
            this.type = param1;
            this.var_3034 = param2;
            this.userId = param3;
            this.titleKey = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.titleKey = param1.ReadUTF();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 6);
            param1.ReadShort();
            this.var_3034 = param1.ReadShort();
            this.type = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.titleKey);
            param1.WriteInt(param1.Shift(this.userId, 26));
            param1.WriteShort(-6843);
            param1.WriteShort(this.var_3034);
            param1.WriteShort(this.type);
            param1.WriteShort(12079);
        }
    }
}
