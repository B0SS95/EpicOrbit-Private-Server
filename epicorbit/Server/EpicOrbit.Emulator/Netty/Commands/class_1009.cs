using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1009 : ICommand {

        public short ID { get; set; } = 11770;
        public int name_128 = 0;
        public int name_149 = 0;
        public float points = 0;
        public int rank = 0;

        public class_1009(int param1 = 0, int param2 = 0, int param3 = 0, float param4 = 0) {
            this.name_128 = param1;
            this.name_149 = param2;
            this.rank = param3;
            this.points = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.name_128 = param1.ReadInt();
            this.name_128 = param1.Shift(this.name_128, 11);
            param1.ReadShort();
            this.name_149 = param1.ReadInt();
            this.name_149 = param1.Shift(this.name_149, 21);
            this.points = param1.ReadFloat();
            this.rank = param1.ReadInt();
            this.rank = param1.Shift(this.rank, 5);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-16314);
            param1.WriteInt(param1.Shift(this.name_128, 21));
            param1.WriteShort(-15112);
            param1.WriteInt(param1.Shift(this.name_149, 11));
            param1.WriteFloat(this.points);
            param1.WriteInt(param1.Shift(this.rank, 27));
        }
    }
}
