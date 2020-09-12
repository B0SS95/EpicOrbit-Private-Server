using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UserSettingsCommand : ICommand {

        public short ID { get; set; } = 6163;
        public DisplaySettingsCommand displaySettingsModule;
        public WindowSettingsModule windowSettingsModule;
        public GameplaySettingsModule gameplaySettingsModule;
        public AudioSettingsModule audioSettingsModule;
        public class_704 var_3182;
        public QualitySettingsModule qualitySettingsModule;

        public UserSettingsCommand(QualitySettingsModule param1 = null, DisplaySettingsCommand param2 = null, AudioSettingsModule param3 = null, WindowSettingsModule param4 = null, GameplaySettingsModule param5 = null, class_704 param6 = null) {
            if (param1 == null) {
                this.qualitySettingsModule = new QualitySettingsModule();
            } else {
                this.qualitySettingsModule = param1;
            }
            if (param2 == null) {
                this.displaySettingsModule = new DisplaySettingsCommand();
            } else {
                this.displaySettingsModule = param2;
            }
            if (param3 == null) {
                this.audioSettingsModule = new AudioSettingsModule();
            } else {
                this.audioSettingsModule = param3;
            }
            if (param4 == null) {
                this.windowSettingsModule = new WindowSettingsModule();
            } else {
                this.windowSettingsModule = param4;
            }
            if (param5 == null) {
                this.gameplaySettingsModule = new GameplaySettingsModule();
            } else {
                this.gameplaySettingsModule = param5;
            }
            if (param6 == null) {
                this.var_3182 = new class_704();
            } else {
                this.var_3182 = param6;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.displaySettingsModule = lookup.Lookup(param1) as DisplaySettingsCommand;
            this.displaySettingsModule.Read(param1, lookup);
            this.windowSettingsModule = lookup.Lookup(param1) as WindowSettingsModule;
            this.windowSettingsModule.Read(param1, lookup);
            param1.ReadShort();
            this.gameplaySettingsModule = lookup.Lookup(param1) as GameplaySettingsModule;
            this.gameplaySettingsModule.Read(param1, lookup);
            param1.ReadShort();
            this.audioSettingsModule = lookup.Lookup(param1) as AudioSettingsModule;
            this.audioSettingsModule.Read(param1, lookup);
            this.var_3182 = lookup.Lookup(param1) as class_704;
            this.var_3182.Read(param1, lookup);
            this.qualitySettingsModule = lookup.Lookup(param1) as QualitySettingsModule;
            this.qualitySettingsModule.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.displaySettingsModule.Write(param1);
            this.windowSettingsModule.Write(param1);
            param1.WriteShort(-13978);
            this.gameplaySettingsModule.Write(param1);
            param1.WriteShort(-19863);
            this.audioSettingsModule.Write(param1);
            this.var_3182.Write(param1);
            this.qualitySettingsModule.Write(param1);
        }
    }
}
