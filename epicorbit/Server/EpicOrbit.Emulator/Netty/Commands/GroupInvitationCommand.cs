using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class GroupInvitationCommand : ICommand {

        public short ID { get; set; } = 11037;
        public string targetName = "";
        public int targetId = 0;
        public class_504 inviterShipIcon;
        public string inviterName = "";
        public class_504 targetShipIcon;
        public int inviterId = 0;

        public GroupInvitationCommand(int param1 = 0, string param2 = "", class_504 param3 = null, int param4 = 0, string param5 = "", class_504 param6 = null) {
            this.inviterId = param1;
            this.inviterName = param2;
            if (param3 == null) {
                this.inviterShipIcon = new class_504();
            } else {
                this.inviterShipIcon = param3;
            }
            this.targetId = param4;
            this.targetName = param5;
            if (param6 == null) {
                this.targetShipIcon = new class_504();
            } else {
                this.targetShipIcon = param6;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.targetName = param1.ReadUTF();
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 8);
            this.inviterShipIcon = lookup.Lookup(param1) as class_504;
            this.inviterShipIcon.Read(param1, lookup);
            this.inviterName = param1.ReadUTF();
            this.targetShipIcon = lookup.Lookup(param1) as class_504;
            this.targetShipIcon.Read(param1, lookup);
            this.inviterId = param1.ReadInt();
            this.inviterId = param1.Shift(this.inviterId, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5882);
            param1.WriteUTF(this.targetName);
            param1.WriteInt(param1.Shift(this.targetId, 24));
            this.inviterShipIcon.Write(param1);
            param1.WriteUTF(this.inviterName);
            this.targetShipIcon.Write(param1);
            param1.WriteInt(param1.Shift(this.inviterId, 2));
        }
    }
}
