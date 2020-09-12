using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AsteroidProgressCommand : ICommand {

        public short ID { get; set; } = 31803;
        public bool buildButtonActive = false;
        public string ownClanName = "";
        public float bestProgress = 0;
        public string bestProgressClanName = "";
        public EquippedModulesModule state;
        public float ownProgress = 0;
        public int battleStationId = 0;

        public AsteroidProgressCommand(int param1 = 0, float param2 = 0, float param3 = 0, string param4 = "", string param5 = "", EquippedModulesModule param6 = null, bool param7 = false) {
            this.battleStationId = param1;
            this.ownProgress = param2;
            this.bestProgress = param3;
            this.ownClanName = param4;
            this.bestProgressClanName = param5;
            if (param6 == null) {
                this.state = new EquippedModulesModule();
            } else {
                this.state = param6;
            }
            this.buildButtonActive = param7;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.buildButtonActive = param1.ReadBoolean();
            this.ownClanName = param1.ReadUTF();
            this.bestProgress = param1.ReadFloat();
            this.bestProgressClanName = param1.ReadUTF();
            param1.ReadShort();
            this.state = lookup.Lookup(param1) as EquippedModulesModule;
            this.state.Read(param1, lookup);
            this.ownProgress = param1.ReadFloat();
            param1.ReadShort();
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.buildButtonActive);
            param1.WriteUTF(this.ownClanName);
            param1.WriteFloat(this.bestProgress);
            param1.WriteUTF(this.bestProgressClanName);
            param1.WriteShort(10025);
            this.state.Write(param1);
            param1.WriteFloat(this.ownProgress);
            param1.WriteShort(7020);
            param1.WriteInt(param1.Shift(this.battleStationId, 24));
        }
    }
}
