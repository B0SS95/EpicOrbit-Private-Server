using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AchievementEarnedCommand : ICommand {

        public const short NO_OFFER = 3;
        public const short USER_NOT_ELIGIBLE_YET = 0;
        public const short USED = 2;
        public const short PENDING = 1;
        public short ID { get; set; } = 31534;
        public bool done = false;
        public int achievementId = 0;
        public short state = 0;

        public AchievementEarnedCommand(short param1 = 0, int param2 = 0, bool param3 = false) {
            this.state = param1;
            this.achievementId = param2;
            this.done = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.done = param1.ReadBoolean();
            this.achievementId = param1.ReadInt();
            this.achievementId = param1.Shift(this.achievementId, 18);
            this.state = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.done);
            param1.WriteInt(param1.Shift(this.achievementId, 14));
            param1.WriteShort(this.state);
        }
    }
}
