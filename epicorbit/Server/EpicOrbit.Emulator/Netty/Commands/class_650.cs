using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_650 : ICommand {

        public const short LASER = 0;
        public const short ROCKET = 1;
        public const short HELLSTORM = 2;
        public const short const_59 = 3;
        public short ID { get; set; } = 18645;
        public int uid = 0;
        public short type = 0;
        public int name_36 = 0;

        public class_650(short param1 = 0, int param2 = 0, int param3 = 0) {
            this.type = param1;
            this.name_36 = param2;
            this.uid = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 28);
            this.type = param1.ReadShort();
            param1.ReadShort();
            this.name_36 = param1.ReadInt();
            this.name_36 = param1.Shift(this.name_36, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.uid, 4));
            param1.WriteShort(this.type);
            param1.WriteShort(32522);
            param1.WriteInt(param1.Shift(this.name_36, 29));
        }
    }
}
