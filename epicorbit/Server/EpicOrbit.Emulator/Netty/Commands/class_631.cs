using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_631 : ICommand {

        public short ID { get; set; } = 2511;
        public int y = 0;
        public int x = 0;
        public float duration = 0;

        public class_631(int param1 = 0, int param2 = 0, float param3 = 0) {
            this.x = param1;
            this.y = param2;
            this.duration = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 20);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 31);
            param1.ReadShort();
            this.duration = param1.ReadFloat();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.y, 12));
            param1.WriteInt(param1.Shift(this.x, 1));
            param1.WriteShort(-16895);
            param1.WriteFloat(this.duration);
        }
    }
}
