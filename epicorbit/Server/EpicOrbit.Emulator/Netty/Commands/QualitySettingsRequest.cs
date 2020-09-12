using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QualitySettingsRequest : ICommand {

        public short ID { get; set; } = 6731;
        public short qualityShip = 0;
        public short qualityCollectables = 0;
        public short qualityAttack = 0;
        public short qualityExplosion = 0;
        public short qualityEngine = 0;
        public short qualityEffect = 0;
        public short qualityPresetting = 0;
        public bool qualityCustomized = false;
        public short qualityPOIzone = 0;
        public short qualityBackground = 0;

        public QualitySettingsRequest(short param1 = 0, short param2 = 0, short param3 = 0, bool param4 = false, short param5 = 0, short param6 = 0, short param7 = 0, short param8 = 0, short param9 = 0, short param10 = 0) {
            this.qualityAttack = param1;
            this.qualityBackground = param2;
            this.qualityPresetting = param3;
            this.qualityCustomized = param4;
            this.qualityPOIzone = param5;
            this.qualityShip = param6;
            this.qualityEngine = param7;
            this.qualityExplosion = param8;
            this.qualityCollectables = param9;
            this.qualityEffect = param10;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.qualityShip = param1.ReadShort();
            this.qualityCollectables = param1.ReadShort();
            this.qualityAttack = param1.ReadShort();
            this.qualityExplosion = param1.ReadShort();
            this.qualityEngine = param1.ReadShort();
            this.qualityEffect = param1.ReadShort();
            this.qualityPresetting = param1.ReadShort();
            this.qualityCustomized = param1.ReadBoolean();
            this.qualityPOIzone = param1.ReadShort();
            this.qualityBackground = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(17173);
            param1.WriteShort(this.qualityShip);
            param1.WriteShort(this.qualityCollectables);
            param1.WriteShort(this.qualityAttack);
            param1.WriteShort(this.qualityExplosion);
            param1.WriteShort(this.qualityEngine);
            param1.WriteShort(this.qualityEffect);
            param1.WriteShort(this.qualityPresetting);
            param1.WriteBoolean(this.qualityCustomized);
            param1.WriteShort(this.qualityPOIzone);
            param1.WriteShort(this.qualityBackground);
        }
    }
}
