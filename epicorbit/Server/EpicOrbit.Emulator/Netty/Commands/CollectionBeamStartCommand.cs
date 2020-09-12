using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CollectionBeamStartCommand : ICommand {

        public short ID { get; set; } = 25345;
        public bool isPlayer = false;
        public int mapObjectId = 0;
        public int duration = 0;

        public CollectionBeamStartCommand(bool param1 = false, int param2 = 0, int param3 = 0) {
            this.isPlayer = param1;
            this.mapObjectId = param2;
            this.duration = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.isPlayer = param1.ReadBoolean();
            this.mapObjectId = param1.ReadInt();
            this.mapObjectId = param1.Shift(this.mapObjectId, 25);
            this.duration = param1.ReadInt();
            this.duration = param1.Shift(this.duration, 11);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.isPlayer);
            param1.WriteInt(param1.Shift(this.mapObjectId, 7));
            param1.WriteInt(param1.Shift(this.duration, 21));
        }
    }
}
