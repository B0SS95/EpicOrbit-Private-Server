using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class StarmapStationCommand : ICommand {

        public short ID { get; set; } = 6336;
        public List<StarmapStationInfo> stations;
        public double currentServerTimestamp = 0;

        public StarmapStationCommand(double param1 = 0, List<StarmapStationInfo> param2 = null) {
            this.currentServerTimestamp = param1;
            if (param2 == null) {
                this.stations = new List<StarmapStationInfo>();
            } else {
                this.stations = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.stations.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as StarmapStationInfo;
                tmp_0.Read(param1, lookup);
                this.stations.Add(tmp_0);
            }
            param1.ReadShort();
            this.currentServerTimestamp = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.stations.Count);
            foreach (var tmp_0 in this.stations) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-2243);
            param1.WriteDouble(this.currentServerTimestamp);
        }
    }
}
