using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_719 : ICommand {

        // ClanMemberOnlineInfoCommand
        // MapEventSetShipAttackableCommand

        public short ID { get; set; } = 23538;
        public int userId = 0;
        public bool var_4111 = false;

        public class_719(int param1 = 0, bool param2 = false) {
            this.userId = param1;
            this.var_4111 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 16);
            this.var_4111 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-2330);
            param1.WriteInt(param1.Shift(this.userId, 16));
            param1.WriteBoolean(this.var_4111);
        }
    }
}
