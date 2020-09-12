using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_964 : ICommand {

        public short ID { get; set; } = 25691;
        public class_1004 item;

        public class_964(class_1004 param1 = null) {
            if (param1 == null) {
                this.item = new class_1004();
            } else {
                this.item = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.item = lookup.Lookup(param1) as class_1004;
            this.item.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-1865);
            this.item.Write(param1);
            param1.WriteShort(-32494);
        }
    }
}
