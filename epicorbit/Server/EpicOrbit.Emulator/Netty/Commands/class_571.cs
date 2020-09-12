using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_571 : class_503, ICommand {

        public override short ID { get; set; } = 22528;
        public bool var_881 = false;
        public int var_4117 = 0;

        public class_571(bool param1 = false, int param2 = 0) {
            this.var_881 = param1;
            this.var_4117 = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_881 = param1.ReadBoolean();
            this.var_4117 = param1.ReadInt();
            this.var_4117 = param1.Shift(this.var_4117, 4);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteBoolean(this.var_881);
            param1.WriteInt(param1.Shift(this.var_4117, 28));
        }
    }
}
