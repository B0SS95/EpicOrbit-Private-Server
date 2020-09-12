using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CountdownStatusCloseWindowCommand : ICommand {

        public short ID { get; set; } = 22032;
        public CountdownStatusTypeModule type;

        public CountdownStatusCloseWindowCommand(CountdownStatusTypeModule param1 = null) {
            if (param1 == null) {
                this.type = new CountdownStatusTypeModule();
            } else {
                this.type = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = lookup.Lookup(param1) as CountdownStatusTypeModule;
            this.type.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.type.Write(param1);
        }
    }
}
