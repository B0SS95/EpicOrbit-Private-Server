using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class VersionRequest : ICommand {

        public short ID { get; set; } = 666;
        public int major = 0;
        public int minor = 0;
        public int build = 0;

        public VersionRequest(int param1 = 0, int param2 = 108, int param3 = 2) {
            this.major = param1;
            this.minor = param2;
            this.build = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.major = param1.ReadInt();
            this.minor = param1.ReadInt();
            this.build = param1.ReadInt();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.major);
            param1.WriteInt(this.minor);
            param1.WriteInt(this.build);
        }
    }
}
