using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_918 : ICommand {

        public short ID { get; set; } = 1059;
        public JackpotArenaMatchResultModule var_357;
        public class_1030 name_130;
        public JackpotArenaMatchResultModule var_1594;
        public bool var_4776 = false;

        public class_918(bool param1 = false, JackpotArenaMatchResultModule param2 = null, JackpotArenaMatchResultModule param3 = null, class_1030 param4 = null) {
            this.var_4776 = param1;
            if (param2 == null) {
                this.var_1594 = new JackpotArenaMatchResultModule();
            } else {
                this.var_1594 = param2;
            }
            if (param3 == null) {
                this.var_357 = new JackpotArenaMatchResultModule();
            } else {
                this.var_357 = param3;
            }
            if (param4 == null) {
                this.name_130 = new class_1030();
            } else {
                this.name_130 = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_357 = lookup.Lookup(param1) as JackpotArenaMatchResultModule;
            this.var_357.Read(param1, lookup);
            this.name_130 = lookup.Lookup(param1) as class_1030;
            this.name_130.Read(param1, lookup);
            param1.ReadShort();
            this.var_1594 = lookup.Lookup(param1) as JackpotArenaMatchResultModule;
            this.var_1594.Read(param1, lookup);
            this.var_4776 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_357.Write(param1);
            this.name_130.Write(param1);
            param1.WriteShort(9996);
            this.var_1594.Write(param1);
            param1.WriteBoolean(this.var_4776);
        }
    }
}
