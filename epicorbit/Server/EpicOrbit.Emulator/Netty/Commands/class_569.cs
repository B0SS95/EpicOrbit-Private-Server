using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_569 : ICommand {

        // SpaceConsumedBatteriesCommand
        // SpaceConsumedRocketsCommand

        public virtual short ID { get; set; } = 24400;
        public double name_115 = 0;
        public double name_145 = 0;
        public double name_22 = 0;

        public class_569(double param1 = 0, double param2 = 0, double param3 = 0) {
            this.name_115 = param1;
            this.name_145 = param2;
            this.name_22 = param3;
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.name_115 = param1.ReadDouble();
            this.name_145 = param1.ReadDouble();
            this.name_22 = param1.ReadDouble();
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteShort(-28598);
            param1.WriteShort(-23544);
            param1.WriteDouble(this.name_115);
            param1.WriteDouble(this.name_145);
            param1.WriteDouble(this.name_22);
        }
    }
}
