using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CooldownStartedCommand : ICommand {

        public short ID { get; set; } = 31068;
        public CooldownTypeModule type;
        public int seconds = 0;

        public CooldownStartedCommand(CooldownTypeModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.type = new CooldownTypeModule();
            } else {
                this.type = param1;
            }
            this.seconds = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = lookup.Lookup(param1) as CooldownTypeModule;
            this.type.Read(param1, lookup);
            this.seconds = param1.ReadInt();
            this.seconds = param1.Shift(this.seconds, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.type.Write(param1);
            param1.WriteInt(param1.Shift(this.seconds, 2));
        }
    }
}
