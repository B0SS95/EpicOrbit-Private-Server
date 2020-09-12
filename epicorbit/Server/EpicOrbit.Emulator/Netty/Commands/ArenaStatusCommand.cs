using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ArenaStatusCommand : ICommand {

        public const short JACKPOT = 0;
        public const short DONE = 5;
        public const short COUNTDOWN = 2;
        public const short DESTROYABLE = 6;
        public const short NONE = 7;
        public const short WAITING_FOR_PLAYERS = 1;
        public const short RADIATION_ACTIVE = 4;
        public const short FIGHTING = 3;
        public const short SCHEDULED = 0;
        public short ID { get; set; } = 12970;
        public int warpWarningOffsetSeconds = 0;
        public int secondsLeftInPhase = 0;
        public short arenaType = 0;
        public int survivors = 0;
        public int opponentId = 0;
        public short status = 0;
        public int participants = 0;
        public string opponentName = "";
        public int currentRound = 0;
        public int opponentInstanceId = 0;

        public ArenaStatusCommand(short param1 = 0, short param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, string param6 = "", int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0) {
            this.arenaType = param1;
            this.status = param2;
            this.currentRound = param3;
            this.survivors = param4;
            this.participants = param5;
            this.opponentName = param6;
            this.opponentId = param7;
            this.opponentInstanceId = param8;
            this.secondsLeftInPhase = param9;
            this.warpWarningOffsetSeconds = param10;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.warpWarningOffsetSeconds = param1.ReadInt();
            this.warpWarningOffsetSeconds = param1.Shift(this.warpWarningOffsetSeconds, 15);
            this.secondsLeftInPhase = param1.ReadInt();
            this.secondsLeftInPhase = param1.Shift(this.secondsLeftInPhase, 7);
            this.arenaType = param1.ReadShort();
            this.survivors = param1.ReadInt();
            this.survivors = param1.Shift(this.survivors, 21);
            this.opponentId = param1.ReadInt();
            this.opponentId = param1.Shift(this.opponentId, 12);
            this.status = param1.ReadShort();
            this.participants = param1.ReadInt();
            this.participants = param1.Shift(this.participants, 15);
            this.opponentName = param1.ReadUTF();
            this.currentRound = param1.ReadInt();
            this.currentRound = param1.Shift(this.currentRound, 27);
            this.opponentInstanceId = param1.ReadInt();
            this.opponentInstanceId = param1.Shift(this.opponentInstanceId, 7);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(374);
            param1.WriteInt(param1.Shift(this.warpWarningOffsetSeconds, 17));
            param1.WriteInt(param1.Shift(this.secondsLeftInPhase, 25));
            param1.WriteShort(this.arenaType);
            param1.WriteInt(param1.Shift(this.survivors, 11));
            param1.WriteInt(param1.Shift(this.opponentId, 20));
            param1.WriteShort(this.status);
            param1.WriteInt(param1.Shift(this.participants, 17));
            param1.WriteUTF(this.opponentName);
            param1.WriteInt(param1.Shift(this.currentRound, 5));
            param1.WriteInt(param1.Shift(this.opponentInstanceId, 25));
        }
    }
}
