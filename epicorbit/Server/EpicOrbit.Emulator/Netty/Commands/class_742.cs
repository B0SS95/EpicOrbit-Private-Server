using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_742 : ICommand {

        // LMCollectBankingMultiplierCommand
        // LMJackpotLeftShipsCommand

        public short ID { get; set; } = 15346;
        public LogMessengerPriorityModule var_150;
        public int var_4362 = 0;

        public class_742(LogMessengerPriorityModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.var_150 = new LogMessengerPriorityModule();
            } else {
                this.var_150 = param1;
            }
            this.var_4362 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_150 = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.var_150.Read(param1, lookup);
            this.var_4362 = param1.ReadInt();
            this.var_4362 = param1.Shift(this.var_4362, 16);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_150.Write(param1);
            param1.WriteInt(param1.Shift(this.var_4362, 16));
        }
    }
}
