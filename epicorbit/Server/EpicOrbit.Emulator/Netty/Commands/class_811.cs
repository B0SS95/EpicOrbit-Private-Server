using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_811 : class_543, ICommand {

        public override short ID { get; set; } = 21769;
        public int name_46 = 0;

        public class_811(int param1 = 0) {
            this.name_46 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.name_46 = param1.ReadInt();
            this.name_46 = param1.Shift(this.name_46, 30);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.name_46, 2));
            param1.WriteShort(-19438);
        }
    }
}
