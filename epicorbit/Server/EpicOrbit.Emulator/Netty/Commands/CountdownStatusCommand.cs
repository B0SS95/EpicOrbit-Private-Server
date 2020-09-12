using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CountdownStatusCommand : ICommand {

        public short ID { get; set; } = 8148;
        public int currentAmount = 0;
        public CountdownStatusTypeModule type;
        public int maxAmount = 0;

        public CountdownStatusCommand(CountdownStatusTypeModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.type = new CountdownStatusTypeModule();
            } else {
                this.type = param1;
            }
            this.currentAmount = param2;
            this.maxAmount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.currentAmount = param1.ReadInt();
            this.currentAmount = param1.Shift(this.currentAmount, 23);
            this.type = lookup.Lookup(param1) as CountdownStatusTypeModule;
            this.type.Read(param1, lookup);
            this.maxAmount = param1.ReadInt();
            this.maxAmount = param1.Shift(this.maxAmount, 24);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.currentAmount, 9));
            this.type.Write(param1);
            param1.WriteInt(param1.Shift(this.maxAmount, 8));
            param1.WriteShort(3209);
        }
    }
}
