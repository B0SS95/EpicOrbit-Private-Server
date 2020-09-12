using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_978 : ICommand {

        public short ID { get; set; } = 879;
        public TechTypeModule var_2416;

        public class_978(TechTypeModule param1 = null) {
            if (param1 == null) {
                this.var_2416 = new TechTypeModule();
            } else {
                this.var_2416 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2416 = lookup.Lookup(param1) as TechTypeModule;
            this.var_2416.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-10749);
            this.var_2416.Write(param1);
        }
    }
}
