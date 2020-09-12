using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1066 : ICommand {

        // ContactsFilter ??
        
        public short ID { get; set; } = 11284;
        public bool showRequests = false;
        public bool var_1442 = false;
        public bool var_1289 = false;
        public bool var_1730 = false;

        public class_1066(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false) {
            this.var_1289 = param1;
            this.var_1730 = param2;
            this.showRequests = param3;
            this.var_1442 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.showRequests = param1.ReadBoolean();
            param1.ReadShort();
            this.var_1442 = param1.ReadBoolean();
            this.var_1289 = param1.ReadBoolean();
            this.var_1730 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.showRequests);
            param1.WriteShort(13712);
            param1.WriteBoolean(this.var_1442);
            param1.WriteBoolean(this.var_1289);
            param1.WriteBoolean(this.var_1730);
        }
    }
}
