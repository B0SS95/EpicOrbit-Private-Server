using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_637 : ICommand {

        public short ID { get; set; } = 23509;
        public int duration = 0;
        public class_507 type;

        public class_637(class_507 param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.type = new class_507();
            } else {
                this.type = param1;
            }
            this.duration = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.duration = param1.ReadInt();
            this.duration = param1.Shift(this.duration, 6);
            this.type = lookup.Lookup(param1) as class_507;
            this.type.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.duration, 26));
            this.type.Write(param1);
        }
    }
}
