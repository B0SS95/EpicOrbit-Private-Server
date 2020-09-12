using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackLaserRunCommand : ICommand {

        public short ID { get; set; } = 23229;
        public int var_247 = 0;
        public bool var_3102 = false;
        public int targetId = 0;
        public int attackerId = 0;
        public int var_3431 = 0;
        public bool var_2659 = false;

        public AttackLaserRunCommand(int param1 = 0, int param2 = 0, int param3 = 0, bool param4 = false, bool param5 = false, int param6 = 0) {
            this.attackerId = param1;
            this.targetId = param2;
            this.var_247 = param3;
            this.var_2659 = param4;
            this.var_3102 = param5;
            this.var_3431 = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_247 = param1.ReadInt();
            this.var_247 = param1.Shift(this.var_247, 11);
            this.var_3102 = param1.ReadBoolean();
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 6);
            this.attackerId = param1.ReadInt();
            this.attackerId = param1.Shift(this.attackerId, 2);
            this.var_3431 = param1.ReadInt();
            this.var_3431 = param1.Shift(this.var_3431, 18);
            this.var_2659 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_247, 21));
            param1.WriteBoolean(this.var_3102);
            param1.WriteInt(param1.Shift(this.targetId, 26));
            param1.WriteInt(param1.Shift(this.attackerId, 30));
            param1.WriteInt(param1.Shift(this.var_3431, 14));
            param1.WriteBoolean(this.var_2659);
        }
    }
}
