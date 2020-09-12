using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_657 : class_503, ICommand {

        public override short ID { get; set; } = 25034;
        public int mapId = 0;
        public int y = 0;
        public int x = 0;

        public class_657(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.mapId = param1;
            this.x = param3;
            this.y = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 20);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 16);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 19);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(17880);
            param1.WriteInt(param1.Shift(this.mapId, 12));
            param1.WriteInt(param1.Shift(this.y, 16));
            param1.WriteInt(param1.Shift(this.x, 13));
            param1.WriteShort(27890);
        }
    }
}
