using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestConditionStateModule : ICommand {

        public short ID { get; set; } = 6862;
        public double currentValue = 0;
        public bool active = false;
        public bool completed = false;

        public QuestConditionStateModule(double param1 = 0, bool param2 = false, bool param3 = false) {
            this.currentValue = param1;
            this.active = param2;
            this.completed = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.currentValue = param1.ReadDouble();
            this.active = param1.ReadBoolean();
            this.completed = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.currentValue);
            param1.WriteBoolean(this.active);
            param1.WriteBoolean(this.completed);
        }
    }
}
