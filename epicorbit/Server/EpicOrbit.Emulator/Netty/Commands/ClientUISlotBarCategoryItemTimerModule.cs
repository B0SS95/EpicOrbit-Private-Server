using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarCategoryItemTimerModule : ICommand {

        public short ID { get; set; } = 23066;
        public double time = 0;
        public string var_2176 = "";
        public bool activatable = false;
        public double maxTime = 0;
        public ClientUISlotBarCategoryItemTimerStateModule timerState;

        public ClientUISlotBarCategoryItemTimerModule(string param1 = "", ClientUISlotBarCategoryItemTimerStateModule param2 = null, double param3 = 0, double param4 = 0, bool param5 = false) {
            this.var_2176 = param1;
            if (param2 == null) {
                this.timerState = new ClientUISlotBarCategoryItemTimerStateModule();
            } else {
                this.timerState = param2;
            }
            this.time = param3;
            this.maxTime = param4;
            this.activatable = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.time = param1.ReadDouble();
            this.var_2176 = param1.ReadUTF();
            this.activatable = param1.ReadBoolean();
            this.maxTime = param1.ReadDouble();
            this.timerState = lookup.Lookup(param1) as ClientUISlotBarCategoryItemTimerStateModule;
            this.timerState.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-11134);
            param1.WriteDouble(this.time);
            param1.WriteUTF(this.var_2176);
            param1.WriteBoolean(this.activatable);
            param1.WriteDouble(this.maxTime);
            this.timerState.Write(param1);
        }
    }
}
