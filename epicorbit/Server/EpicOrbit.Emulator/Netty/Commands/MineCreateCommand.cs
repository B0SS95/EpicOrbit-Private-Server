using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MineCreateCommand : class_752, ICommand {

        public override short ID { get; set; } = 19870;
        public bool red = false;
        public int typeId = 0;
        public bool massiveExplosion = false;

        public MineCreateCommand(string param1 = "", bool param2 = false, bool param3 = false, int param4 = 0, int param5 = 0, int param6 = 0)
         : base(param1, param6, param5) {
            this.typeId = param4;
            this.red = param2;
            this.massiveExplosion = param3;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.red = param1.ReadBoolean();
            param1.ReadShort();
            this.typeId = param1.ReadInt();
            this.typeId = param1.Shift(this.typeId, 10);
            this.massiveExplosion = param1.ReadBoolean();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteBoolean(this.red);
            param1.WriteShort(21191);
            param1.WriteInt(param1.Shift(this.typeId, 22));
            param1.WriteBoolean(this.massiveExplosion);
        }
    }
}
