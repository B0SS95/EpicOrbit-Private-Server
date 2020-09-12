using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DroneFormationChangeCommand : ICommand {

        public short ID { get; set; } = 15628;
        public int formation = 0;
        public int uid = 0;

        public DroneFormationChangeCommand(int param1 = 0, int param2 = 0) {
            this.uid = param1;
            this.formation = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.formation = param1.ReadInt();
            this.formation = param1.Shift(this.formation, 17);
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 9);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5957);
            param1.WriteShort(-6885);
            param1.WriteInt(param1.Shift(this.formation, 15));
            param1.WriteInt(param1.Shift(this.uid, 23));
        }
    }
}
