using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1073 : ICommand {

        public short ID { get; set; } = 667;
        public int major = 0;
        public int minor = 0;
        public int build = 0;
        public bool name_174 = false;

        public class_1073(int param1 = 0, int param2 = 108, int param3 = 2, bool param4 = false) {
            this.major = param1;
            this.minor = param2;
            this.build = param3;
            this.name_174 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.major = param1.ReadInt();
            this.minor = param1.ReadInt();
            this.build = param1.ReadInt();
            this.name_174 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.major);
            param1.WriteInt(this.minor);
            param1.WriteInt(this.build);
            param1.WriteBoolean(this.name_174);
        }
    }
}
