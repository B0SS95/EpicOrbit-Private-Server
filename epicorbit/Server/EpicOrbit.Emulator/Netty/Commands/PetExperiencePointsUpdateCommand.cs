using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetExperiencePointsUpdateCommand : class_547, ICommand {

        public override short ID { get; set; } = 26757;
        public double maxExperiencePoints = 0;
        public double currentExperiencePoints = 0;

        public PetExperiencePointsUpdateCommand(double param1 = 0, double param2 = 0) {
            this.currentExperiencePoints = param1;
            this.maxExperiencePoints = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.maxExperiencePoints = param1.ReadDouble();
            this.currentExperiencePoints = param1.ReadDouble();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteDouble(this.maxExperiencePoints);
            param1.WriteDouble(this.currentExperiencePoints);
            param1.WriteShort(-16380);
        }
    }
}
