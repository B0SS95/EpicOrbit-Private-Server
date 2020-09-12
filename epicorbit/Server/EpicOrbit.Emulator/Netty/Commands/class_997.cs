using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_997 : ICommand {

        public short ID { get; set; } = 19636;
        public int var_1447 = 0;
        public int mapId = 0;

        public class_997(int param1 = 0, int param2 = 0) {
            this.mapId = param1;
            this.var_1447 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1447 = param1.ReadInt();
            this.var_1447 = param1.Shift(this.var_1447, 20);
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 7);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1447, 12));
            param1.WriteInt(param1.Shift(this.mapId, 25));
        }
    }
}
