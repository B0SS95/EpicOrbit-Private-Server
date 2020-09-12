using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMCollectExperiencePointsCommand : ICommand {

        public short ID { get; set; } = 24549;
        public int summedAmount = 0;
        public int collectedAmount = 0;
        public LogMessengerPriorityModule priorityMode;
        public int currentLevel = 0;

        public LMCollectExperiencePointsCommand(LogMessengerPriorityModule param1 = null, int param2 = 0, int param3 = 0, int param4 = 0) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.collectedAmount = param2;
            this.summedAmount = param3;
            this.currentLevel = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.summedAmount = param1.ReadInt();
            this.summedAmount = param1.Shift(this.summedAmount, 4);
            this.collectedAmount = param1.ReadInt();
            this.collectedAmount = param1.Shift(this.collectedAmount, 31);
            param1.ReadShort();
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            this.currentLevel = param1.ReadInt();
            this.currentLevel = param1.Shift(this.currentLevel, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.summedAmount, 28));
            param1.WriteInt(param1.Shift(this.collectedAmount, 1));
            param1.WriteShort(8771);
            this.priorityMode.Write(param1);
            param1.WriteInt(param1.Shift(this.currentLevel, 2));
        }
    }
}
