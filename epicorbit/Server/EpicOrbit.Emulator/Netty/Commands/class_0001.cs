using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_0001 : ICommand {

        public short ID { get; set; } = 7892;
        public string playerName = "";
        public int damageRecieved = 0;
        public int peakDamage = 0;
        public int damageDealt = 0;
        public int durationInSeconds = 0;

        public class_0001(string param1 = "", int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.playerName = param1;
            this.durationInSeconds = param2;
            this.damageDealt = param3;
            this.damageRecieved = param4;
            this.peakDamage = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playerName = param1.ReadUTF();
            this.damageRecieved = param1.ReadInt();
            this.damageRecieved = param1.Shift(this.damageRecieved, 17);
            param1.ReadShort();
            this.peakDamage = param1.ReadInt();
            this.peakDamage = param1.Shift(this.peakDamage, 12);
            this.damageDealt = param1.ReadInt();
            this.damageDealt = param1.Shift(this.damageDealt, 13);
            this.durationInSeconds = param1.ReadInt();
            this.durationInSeconds = param1.Shift(this.durationInSeconds, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.playerName);
            param1.WriteInt(param1.Shift(this.damageRecieved, 15));
            param1.WriteShort(10887);
            param1.WriteInt(param1.Shift(this.peakDamage, 20));
            param1.WriteInt(param1.Shift(this.damageDealt, 19));
            param1.WriteInt(param1.Shift(this.durationInSeconds, 10));
        }
    }
}
