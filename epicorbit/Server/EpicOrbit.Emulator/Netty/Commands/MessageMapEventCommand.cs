using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MessageMapEventCommand : ICommand {

        public const short STANDARD = 0;
        public short ID { get; set; } = 10229;
        public int priority = 0;
        public string message = "";
        public short type = 0;
        public List<string> replacementObjectList;

        public MessageMapEventCommand(short param1 = 0, int param2 = 0, string param3 = "", List<string> param4 = null) {
            this.type = param1;
            this.priority = param2;
            this.message = param3;
            if (param4 == null) {
                this.replacementObjectList = new List<String>();
            } else {
                this.replacementObjectList = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.priority = param1.ReadInt();
            this.priority = param1.Shift(this.priority, 10);
            this.message = param1.ReadUTF();
            this.type = param1.ReadShort();
            this.replacementObjectList.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.ReadUTF();
                this.replacementObjectList.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.priority, 22));
            param1.WriteUTF(this.message);
            param1.WriteShort(this.type);
            param1.WriteInt(this.replacementObjectList.Count);
            foreach (var tmp_0 in this.replacementObjectList) {
                param1.WriteUTF(tmp_0);
            }
        }
    }
}
