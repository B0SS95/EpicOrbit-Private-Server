using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1072 : class_698, ICommand {

        public const short const_3009 = 0;
        public const short const_2303 = 1;
        public const short const_2274 = 2;
        public override short ID { get; set; } = 18727;
        public short replacement = 0;
        public int mapId = 0;

        public class_1072(short param1 = 0, int param2 = 0) {
            this.mapId = param2;
            this.replacement = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.replacement = param1.ReadShort();
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 4);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-14890);
            param1.WriteShort(this.replacement);
            param1.WriteInt(param1.Shift(this.mapId, 28));
            param1.WriteShort(4902);
        }
    }
}
