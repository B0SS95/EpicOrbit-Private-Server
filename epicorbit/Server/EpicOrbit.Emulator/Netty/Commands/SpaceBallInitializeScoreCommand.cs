using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SpaceBallInitializeScoreCommand : ICommand {

        public short ID { get; set; } = 15601;
        public int scoreVenus = 0;
        public int scoreEarth = 0;
        public int ownerFactionId = 0;
        public int scoreMars = 0;
        public int speed = 0;

        public SpaceBallInitializeScoreCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.scoreMars = param1;
            this.scoreEarth = param2;
            this.scoreVenus = param3;
            this.ownerFactionId = param4;
            this.speed = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.scoreVenus = param1.ReadInt();
            this.scoreVenus = param1.Shift(this.scoreVenus, 24);
            this.scoreEarth = param1.ReadInt();
            this.scoreEarth = param1.Shift(this.scoreEarth, 24);
            this.ownerFactionId = param1.ReadInt();
            this.ownerFactionId = param1.Shift(this.ownerFactionId, 30);
            param1.ReadShort();
            this.scoreMars = param1.ReadInt();
            this.scoreMars = param1.Shift(this.scoreMars, 12);
            this.speed = param1.ReadInt();
            this.speed = param1.Shift(this.speed, 24);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.scoreVenus, 8));
            param1.WriteInt(param1.Shift(this.scoreEarth, 8));
            param1.WriteInt(param1.Shift(this.ownerFactionId, 2));
            param1.WriteShort(3257);
            param1.WriteInt(param1.Shift(this.scoreMars, 20));
            param1.WriteInt(param1.Shift(this.speed, 8));
        }
    }
}
