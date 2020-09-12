using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BeaconCommand : ICommand {

        public short ID { get; set; } = 22361;
        public int var_4576 = 0;
        public bool protectionZoneActive = false;
        public int var_3778 = 0;
        public int var_3470 = 0;
        public bool hitpointsLowWarning = false;
        public bool radiationWarning = false;
        public bool repairBotActive = false;
        public string repairBotName = "";
        public int var_19 = 0;

        public BeaconCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, bool param5 = false, bool param6 = false, bool param7 = false, string param8 = "", bool param9 = false) {
            this.var_3470 = param1;
            this.var_19 = param2;
            this.var_3778 = param3;
            this.var_4576 = param4;
            this.protectionZoneActive = param5;
            this.repairBotActive = param6;
            this.hitpointsLowWarning = param7;
            this.repairBotName = param8;
            this.radiationWarning = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4576 = param1.ReadInt();
            this.var_4576 = param1.Shift(this.var_4576, 16);
            this.protectionZoneActive = param1.ReadBoolean();
            this.var_3778 = param1.ReadInt();
            this.var_3778 = param1.Shift(this.var_3778, 15);
            this.var_3470 = param1.ReadInt();
            this.var_3470 = param1.Shift(this.var_3470, 11);
            this.hitpointsLowWarning = param1.ReadBoolean();
            param1.ReadShort();
            this.radiationWarning = param1.ReadBoolean();
            this.repairBotActive = param1.ReadBoolean();
            this.repairBotName = param1.ReadUTF();
            this.var_19 = param1.ReadInt();
            this.var_19 = param1.Shift(this.var_19, 1);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_4576, 16));
            param1.WriteBoolean(this.protectionZoneActive);
            param1.WriteInt(param1.Shift(this.var_3778, 17));
            param1.WriteInt(param1.Shift(this.var_3470, 21));
            param1.WriteBoolean(this.hitpointsLowWarning);
            param1.WriteShort(10041);
            param1.WriteBoolean(this.radiationWarning);
            param1.WriteBoolean(this.repairBotActive);
            param1.WriteUTF(this.repairBotName);
            param1.WriteInt(param1.Shift(this.var_19, 31));
            param1.WriteShort(2544);
        }
    }
}
