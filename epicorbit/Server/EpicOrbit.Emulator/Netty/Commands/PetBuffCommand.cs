using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetBuffCommand : ICommand {

        public const short REMOVE = 1;
        public const short ADD = 0;
        public short ID { get; set; } = 32306;
        public short effectId = 0;
        public List<int> addingParameters;
        public short effectAction = 0;

        public PetBuffCommand(short param1 = 0, short param2 = 0, List<int> param3 = null) {
            this.effectAction = param1;
            this.effectId = param2;
            if (param3 == null) {
                this.addingParameters = new List<int>();
            } else {
                this.addingParameters = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.effectId = param1.ReadShort();
            this.addingParameters.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 27);
                this.addingParameters.Add(tmp_0);
            }
            this.effectAction = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-2866);
            param1.WriteShort(this.effectId);
            param1.WriteInt(this.addingParameters.Count);
            foreach (var tmp_0 in this.addingParameters) {
                param1.WriteInt(param1.Shift(tmp_0, 5));
            }
            param1.WriteShort(this.effectAction);
            param1.WriteShort(-25025);
        }
    }
}
