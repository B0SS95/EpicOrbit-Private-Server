using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PerformanceTrackingSendInfoRequest : ICommand {

        public short ID { get; set; } = 27188;
        public List<int> optionalPerformanceValues;
        public int memoryUsage = 0;
        public string version = "";
        public int fps = 0;

        public PerformanceTrackingSendInfoRequest(string param1 = "", int param2 = 0, int param3 = 0, List<int> param4 = null) {
            this.version = param1;
            this.fps = param2;
            this.memoryUsage = param3;
            if (param4 == null) {
                this.optionalPerformanceValues = new List<int>();
            } else {
                this.optionalPerformanceValues = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.optionalPerformanceValues.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 13);
                this.optionalPerformanceValues.Add(tmp_0);
            }
            this.memoryUsage = param1.ReadInt();
            this.memoryUsage = param1.Shift(this.memoryUsage, 4);
            this.version = param1.ReadUTF();
            this.fps = param1.ReadInt();
            this.fps = param1.Shift(this.fps, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.optionalPerformanceValues.Count);
            foreach (var tmp_0 in this.optionalPerformanceValues) {
                param1.WriteInt(param1.Shift(tmp_0, 19));
            }
            param1.WriteInt(param1.Shift(this.memoryUsage, 28));
            param1.WriteUTF(this.version);
            param1.WriteInt(param1.Shift(this.fps, 20));
        }
    }
}
