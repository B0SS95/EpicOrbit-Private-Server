using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_992 : ICommand {

        public short ID { get; set; } = 15572;
        public int var_4086 = 0;
        public LogMessengerPriorityModule var_150;

        public class_992(LogMessengerPriorityModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.var_150 = new LogMessengerPriorityModule();
            } else {
                this.var_150 = param1;
            }
            this.var_4086 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4086 = param1.ReadInt();
            this.var_4086 = param1.Shift(this.var_4086, 10);
            this.var_150 = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.var_150.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_4086, 22));
            this.var_150.Write(param1);
        }
    }
}
