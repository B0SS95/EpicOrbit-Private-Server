using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QualitySettingsModule : ICommand {

        public short ID { get; set; } = 4314;
        public short qualityAttack = 0;
        public bool qualityCustomized = false;
        public bool notSet = false;
        public short qualityCollectables = 0;
        public short qualityShip = 0;
        public short qualityEffect = 0;
        public short qualityPresetting = 0;
        public short qualityExplosion = 0;
        public short qualityEngine = 0;
        public short qualityBackground = 0;
        public short qualityPOIzone = 0;

        public QualitySettingsModule(bool param1 = false, short param2 = 0, short param3 = 0, short param4 = 0, bool param5 = false, short param6 = 0, short param7 = 0, short param8 = 0, short param9 = 0, short param10 = 0, short param11 = 0) {
            this.notSet = param1;
            this.qualityAttack = param2;
            this.qualityBackground = param3;
            this.qualityPresetting = param4;
            this.qualityCustomized = param5;
            this.qualityPOIzone = param6;
            this.qualityShip = param7;
            this.qualityEngine = param8;
            this.qualityExplosion = param9;
            this.qualityCollectables = param10;
            this.qualityEffect = param11;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.qualityAttack = param1.ReadShort();
            this.qualityCustomized = param1.ReadBoolean();
            this.notSet = param1.ReadBoolean();
            this.qualityCollectables = param1.ReadShort();
            this.qualityShip = param1.ReadShort();
            param1.ReadShort();
            this.qualityEffect = param1.ReadShort();
            this.qualityPresetting = param1.ReadShort();
            this.qualityExplosion = param1.ReadShort();
            this.qualityEngine = param1.ReadShort();
            this.qualityBackground = param1.ReadShort();
            this.qualityPOIzone = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.qualityAttack);
            param1.WriteBoolean(this.qualityCustomized);
            param1.WriteBoolean(this.notSet);
            param1.WriteShort(this.qualityCollectables);
            param1.WriteShort(this.qualityShip);
            param1.WriteShort(21644);
            param1.WriteShort(this.qualityEffect);
            param1.WriteShort(this.qualityPresetting);
            param1.WriteShort(this.qualityExplosion);
            param1.WriteShort(this.qualityEngine);
            param1.WriteShort(this.qualityBackground);
            param1.WriteShort(this.qualityPOIzone);
        }
    }
}
