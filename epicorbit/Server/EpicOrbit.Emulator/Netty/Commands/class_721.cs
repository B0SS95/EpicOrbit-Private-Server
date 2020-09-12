using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_721 : ICommand {

        // QuestListRemoveCommand
        // DroneFormationAvailableFormationsCommand
        // ClanMembersOnlineIntitialisationCommand

        public short ID { get; set; } = 23880;
        public List<int> var_5253;

        public class_721(List<int> param1 = null) {
            if (param1 == null) {
                this.var_5253 = new List<int>();
            } else {
                this.var_5253 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.var_5253.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 27);
                this.var_5253.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-2472);
            param1.WriteShort(5520);
            param1.WriteInt(this.var_5253.Count);
            foreach (var tmp_0 in this.var_5253) {
                param1.WriteInt(param1.Shift(tmp_0, 5));
            }
        }
    }
}
