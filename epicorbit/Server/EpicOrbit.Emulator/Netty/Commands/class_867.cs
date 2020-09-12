using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_867 : ICommand {

        public const short const_3445 = 1;
        public const short const_728 = 0;
        public short ID { get; set; } = 9871;
        public List<class_617> var_1035;
        public short rewardType = 0;
        public int var_1372 = 0;

        public class_867(List<class_617> param1 = null, int param2 = 0, short param3 = 0) {
            if (param1 == null) {
                this.var_1035 = new List<class_617>();
            } else {
                this.var_1035 = param1;
            }
            this.var_1372 = param2;
            this.rewardType = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1035.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_617;
                tmp_0.Read(param1, lookup);
                this.var_1035.Add(tmp_0);
            }
            this.rewardType = param1.ReadShort();
            this.var_1372 = param1.ReadInt();
            this.var_1372 = param1.Shift(this.var_1372, 7);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_1035.Count);
            foreach (var tmp_0 in this.var_1035) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(this.rewardType);
            param1.WriteInt(param1.Shift(this.var_1372, 25));
        }
    }
}
