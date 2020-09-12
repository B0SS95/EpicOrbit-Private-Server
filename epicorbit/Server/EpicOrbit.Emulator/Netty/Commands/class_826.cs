using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_826 : ICommand {

        // TechDeactivationCommand
        // TechActivationCommand

        public short ID { get; set; } = 19590;
        public int userId = 0;
        public TechTypeModule var_4408;

        public class_826(TechTypeModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.var_4408 = new TechTypeModule();
            } else {
                this.var_4408 = param1;
            }
            this.userId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 27);
            this.var_4408 = lookup.Lookup(param1) as TechTypeModule;
            this.var_4408.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.userId, 5));
            this.var_4408.Write(param1);
            param1.WriteShort(4304);
        }
    }
}
