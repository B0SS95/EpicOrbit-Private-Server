using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_928 : class_505, ICommand {

        public const short const_515 = 2;
        public const short const_2161 = 0;
        public const short const_1407 = 1;
        public override short ID { get; set; } = 17072;
        public int var_360 = 0;
        public float playerScore = 0;
        public float var_3870 = 0;
        public short var_3234 = 0;
        public List<LootModule> reward;
        public string name_165 = "";
        public int var_2696 = 0;

        public class_928(List<LootModule> param1 = null, short param2 = 0, int param3 = 0, float param4 = 0, string param5 = "", int param6 = 0, float param7 = 0) {
            this.var_3234 = param2;
            this.var_2696 = param6;
            this.var_360 = param3;
            this.name_165 = param5;
            this.playerScore = param7;
            this.var_3870 = param4;
            if (param1 == null) {
                this.reward = new List<LootModule>();
            } else {
                this.reward = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_360 = param1.ReadInt();
            this.var_360 = param1.Shift(this.var_360, 17);
            this.playerScore = param1.ReadFloat();
            this.var_3870 = param1.ReadFloat();
            this.var_3234 = param1.ReadShort();
            this.reward.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as LootModule;
                tmp_0.Read(param1, lookup);
                this.reward.Add(tmp_0);
            }
            this.name_165 = param1.ReadUTF();
            this.var_2696 = param1.ReadInt();
            this.var_2696 = param1.Shift(this.var_2696, 19);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_360, 15));
            param1.WriteFloat(this.playerScore);
            param1.WriteFloat(this.var_3870);
            param1.WriteShort(this.var_3234);
            param1.WriteInt(this.reward.Count);
            foreach (var tmp_0 in this.reward) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.name_165);
            param1.WriteInt(param1.Shift(this.var_2696, 13));
            param1.WriteShort(-16717);
        }
    }
}
