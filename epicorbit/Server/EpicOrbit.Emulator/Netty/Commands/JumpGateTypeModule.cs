using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class JumpGateTypeModule : ICommand {

        public const short const_2815 = 20;
        public const short GALAXY_GATE = 3;
        public const short HOME_GATE = 1;
        public const short INVISIBLE = 18;
        public const short INVASIONMAP_GATE = 13;
        public const short const_1224 = 22;
        public const short GALAXY_GATE_5 = 8;
        public const short INVASIONMAP_GATE_1 = 14;
        public const short INVASIONMAP_GATE_3 = 16;
        public const short RANDOM_GATE = 2;
        public const short GALAXY_GATE_1 = 4;
        public const short TEAM_DEATHMATCH_1 = 11;
        public const short NORMAL = 0;
        public const short INVASIONMAP_GATE_2 = 15;
        public const short const_2470 = 21;
        public const short GROUP_GATE = 9;
        public const short GALAXY_GATE_3 = 6;
        public const short TEAM_DEATHMATCH = 10;
        public const short GALAXY_GATE_2 = 5;
        public const short INVASIONMAP_LVL_GATE = 17;
        public const short GALAXY_GATE_4 = 7;
        public const short TEAM_DEATHMATCH_2 = 12;
        public const short const_2289 = 19;
        public short ID { get; set; } = 16265;
        public short typeValue = 0;

        public JumpGateTypeModule(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.typeValue);
        }
    }
}
