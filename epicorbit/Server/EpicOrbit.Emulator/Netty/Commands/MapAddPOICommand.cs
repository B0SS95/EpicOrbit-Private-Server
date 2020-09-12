using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapAddPOICommand : ICommand {

        public const int CIRCLE = 0;
        public const int POLYGON = 1;
        public const int RECTANGLE = 2;

        public virtual short ID { get; set; } = 5371;
        public short shape = 0;
        public string poiTypeSpecification = "";
        public string poiId = "";
        public POITypeModule poiType;
        public bool inverted = false;
        public bool active = false;
        public List<int> shapeCoordinates;
        public POIDesignModule design;

        public MapAddPOICommand(string param1 = "", POITypeModule param2 = null, string param3 = "", POIDesignModule param4 = null, short param5 = 0, List<int> param6 = null, bool param7 = false, bool param8 = false) {
            this.poiId = param1;
            if (param2 == null) {
                this.poiType = new POITypeModule();
            } else {
                this.poiType = param2;
            }
            this.poiTypeSpecification = param3;
            if (param4 == null) {
                this.design = new POIDesignModule();
            } else {
                this.design = param4;
            }
            this.shape = param5;
            if (param6 == null) {
                this.shapeCoordinates = new List<int>();
            } else {
                this.shapeCoordinates = param6;
            }
            this.inverted = param7;
            this.active = param8;
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            this.shape = param1.ReadShort();
            param1.ReadShort();
            this.poiTypeSpecification = param1.ReadUTF();
            this.poiId = param1.ReadUTF();
            this.poiType = lookup.Lookup(param1) as POITypeModule;
            this.poiType.Read(param1, lookup);
            this.inverted = param1.ReadBoolean();
            this.active = param1.ReadBoolean();
            this.shapeCoordinates.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 5);
                this.shapeCoordinates.Add(tmp_0);
            }
            this.design = lookup.Lookup(param1) as POIDesignModule;
            this.design.Read(param1, lookup);
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteShort(this.shape);
            param1.WriteShort(-811);
            param1.WriteUTF(this.poiTypeSpecification);
            param1.WriteUTF(this.poiId);
            this.poiType.Write(param1);
            param1.WriteBoolean(this.inverted);
            param1.WriteBoolean(this.active);
            param1.WriteInt(this.shapeCoordinates.Count);
            foreach (var tmp_0 in this.shapeCoordinates) {
                param1.WriteInt(param1.Shift(tmp_0, 27));
            }
            this.design.Write(param1);
        }
    }
}
