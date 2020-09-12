using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class JumpCPUPriceMappingModule : ICommand {

        public short ID { get; set; } = 3873;
        public List<int> mapIdList;
        public int price = 0;

        public JumpCPUPriceMappingModule(int param1 = 0, List<int> param2 = null) {
            this.price = param1;
            if (param2 == null) {
                this.mapIdList = new List<int>();
            } else {
                this.mapIdList = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.mapIdList.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 5);
                this.mapIdList.Add(tmp_0);
            }
            this.price = param1.ReadInt();
            this.price = param1.Shift(this.price, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(19954);
            param1.WriteInt(this.mapIdList.Count);
            foreach (var tmp_0 in this.mapIdList) {
                param1.WriteInt(param1.Shift(tmp_0, 27));
            }
            param1.WriteInt(param1.Shift(this.price, 20));
        }
    }
}
