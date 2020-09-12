using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AudioSettingsRequest : ICommand {

        public short ID { get; set; } = 17759;
        public int sound = 0;
        public int music = 0;
        public int var_957 = 0;
        public bool playCombatMusic = false;

        public AudioSettingsRequest(int param1 = 0, int param2 = 0, int param3 = 0, bool param4 = false) {
            this.sound = param1;
            this.music = param2;
            this.var_957 = param3;
            this.playCombatMusic = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.sound = param1.ReadInt();
            this.sound = param1.Shift(this.sound, 23);
            this.music = param1.ReadInt();
            this.music = param1.Shift(this.music, 13);
            this.var_957 = param1.ReadInt();
            this.var_957 = param1.Shift(this.var_957, 10);
            this.playCombatMusic = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-27206);
            param1.WriteInt(param1.Shift(this.sound, 9));
            param1.WriteInt(param1.Shift(this.music, 19));
            param1.WriteInt(param1.Shift(this.var_957, 22));
            param1.WriteBoolean(this.playCombatMusic);
        }
    }
}
