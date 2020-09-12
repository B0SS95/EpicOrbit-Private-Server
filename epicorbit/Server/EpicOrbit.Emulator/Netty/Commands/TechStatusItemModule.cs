using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class TechStatusItemModule : ICommand {

        public short ID { get; set; } = 28840;
        public int seconds = 0;
        public StatusModule techStatus;
        public int amount = 0;

        public TechStatusItemModule(StatusModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.techStatus = new StatusModule();
            } else {
                this.techStatus = param1;
            }
            this.amount = param2;
            this.seconds = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.seconds = param1.ReadInt();
            this.seconds = param1.Shift(this.seconds, 13);
            param1.ReadShort();
            this.techStatus = lookup.Lookup(param1) as StatusModule;
            this.techStatus.Read(param1, lookup);
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 18);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.seconds, 19));
            param1.WriteShort(-31060);
            this.techStatus.Write(param1);
            param1.WriteInt(param1.Shift(this.amount, 14));
        }
    }
}
