using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CooldownReachedCommand : ICommand {

        public short ID { get; set; } = 22761;
        public CooldownTypeModule type;

        public CooldownReachedCommand(CooldownTypeModule param1 = null) {
            if (param1 == null) {
                this.type = new CooldownTypeModule();
            } else {
                this.type = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = lookup.Lookup(param1) as CooldownTypeModule;
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
