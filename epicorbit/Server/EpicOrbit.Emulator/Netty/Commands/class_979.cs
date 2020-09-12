using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_979 : ICommand {

        public short ID { get; set; } = 2848;
        public int var_3141 = 0;
        public int var_3133 = 0;
        public LogMessengerPriorityModule var_150;

        public class_979(LogMessengerPriorityModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.var_150 = new LogMessengerPriorityModule();
            } else {
                this.var_150 = param1;
            }
            this.var_3141 = param2;
            this.var_3133 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3141 = param1.ReadInt();
            this.var_3141 = param1.Shift(this.var_3141, 4);
            this.var_3133 = param1.ReadInt();
            this.var_3133 = param1.Shift(this.var_3133, 9);
            param1.ReadShort();
            this.var_150 = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.var_150.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_3141, 28));
            param1.WriteInt(param1.Shift(this.var_3133, 23));
            param1.WriteShort(28226);
            this.var_150.Write(param1);
        }
    }
}
