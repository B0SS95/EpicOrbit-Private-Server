using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AudioSettingsModule : ICommand {

        public short ID { get; set; } = 12126;
        public bool playCombatMusic = false;
        public int var_957 = 0;
        public bool var_2006 = false;
        public int sound = 0;
        public int music = 0;

        public AudioSettingsModule(bool param1 = false, int param2 = 0, int param3 = 0, int param4 = 0, bool param5 = false) {
            this.var_2006 = param1;
            this.sound = param2;
            this.music = param3;
            this.var_957 = param4;
            this.playCombatMusic = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playCombatMusic = param1.ReadBoolean();
            this.var_957 = param1.ReadInt();
            this.var_957 = param1.Shift(this.var_957, 12);
            this.var_2006 = param1.ReadBoolean();
            this.sound = param1.ReadInt();
            this.sound = param1.Shift(this.sound, 10);
            this.music = param1.ReadInt();
            this.music = param1.Shift(this.music, 29);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.playCombatMusic);
            param1.WriteInt(param1.Shift(this.var_957, 20));
            param1.WriteBoolean(this.var_2006);
            param1.WriteInt(param1.Shift(this.sound, 22));
            param1.WriteInt(param1.Shift(this.music, 3));
        }
    }
}
