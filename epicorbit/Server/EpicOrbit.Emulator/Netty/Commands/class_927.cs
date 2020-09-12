using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_927 : ICommand {

        public short ID { get; set; } = 11337;
        public int userId = 0;
        public ClanRelationModule var_672;
        public int name_46 = 0;

        public class_927(ClanRelationModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.var_672 = new ClanRelationModule();
            } else {
                this.var_672 = param1;
            }
            this.userId = param2;
            this.name_46 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 10);
            this.var_672 = lookup.Lookup(param1) as ClanRelationModule;
            this.var_672.Read(param1, lookup);
            this.name_46 = param1.ReadInt();
            this.name_46 = param1.Shift(this.name_46, 20);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(766);
            param1.WriteInt(param1.Shift(this.userId, 22));
            this.var_672.Write(param1);
            param1.WriteInt(param1.Shift(this.name_46, 12));
        }
    }
}
