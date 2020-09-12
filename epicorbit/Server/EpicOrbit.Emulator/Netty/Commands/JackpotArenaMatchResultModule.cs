using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class JackpotArenaMatchResultModule : ArenaMatchResultModule, ICommand {

        public override short ID { get; set; } = 24293;
        public int jackpotWins = 0;
        public int jackpotLosses = 0;
        public float jackpotWinRate = 0;

        public JackpotArenaMatchResultModule(int param1 = 0, int param2 = 0, string param3 = "", float param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0)
         : base(param3, param2, param1, param5, param6) {
            this.jackpotLosses = param8;
            this.jackpotWins = param7;
            this.jackpotWinRate = param4;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.jackpotWins = param1.ReadInt();
            this.jackpotWins = param1.Shift(this.jackpotWins, 8);
            this.jackpotLosses = param1.ReadInt();
            this.jackpotLosses = param1.Shift(this.jackpotLosses, 15);
            this.jackpotWinRate = param1.ReadFloat();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.jackpotWins, 24));
            param1.WriteInt(param1.Shift(this.jackpotLosses, 17));
            param1.WriteFloat(this.jackpotWinRate);
        }
    }
}
