using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class EventActivationStateCommand : ICommand {

        public const short const_3303 = 4;
        public const short const_1589 = 16;
        public const short const_305 = 14;
        public const short const_1487 = 12;
        public const short DEMOLISHED_HQ = 8;
        public const short APRIL_FOOLS = 3;
        public const short const_2717 = 6;
        public const short CHRISTMAS_TREES = 1;
        public const short CARNIVAL_2013 = 2;
        public const short const_2521 = 10;
        public const short FROSTED_GATES = 0;
        public const short const_2694 = 15;
        public const short const_2769 = 13;
        public const short const_2716 = 9;
        public const short const_3187 = 5;
        public const short const_1971 = 7;
        public const short const_1314 = 11;
        public short ID { get; set; } = 12882;
        public short type = 0;
        public List<class_698> attributes;
        public bool active = false;

        public EventActivationStateCommand(short param1 = 0, List<class_698> param2 = null, bool param3 = false) {
            this.type = param1;
            if (param2 == null) {
                this.attributes = new List<class_698>();
            } else {
                this.attributes = param2;
            }
            this.active = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
            this.attributes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_698;
                tmp_0.Read(param1, lookup);
                this.attributes.Add(tmp_0);
            }
            param1.ReadShort();
            this.active = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
            param1.WriteInt(this.attributes.Count);
            foreach (var tmp_0 in this.attributes) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(29847);
            param1.WriteBoolean(this.active);
        }
    }
}
