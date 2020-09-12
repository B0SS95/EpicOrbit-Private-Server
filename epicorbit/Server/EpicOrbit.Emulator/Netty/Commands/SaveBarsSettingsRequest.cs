using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SaveBarsSettingsRequest : ICommand {

        public short ID { get; set; } = 25141;
        public string standardSlotBarLayout = "";
        public string genericFeatureBarLayout = "";
        public string barState = "";
        public bool var_505 = false;
        public string proActionBarPosition = "";
        public string premiumSlotBarLayout = "";
        public string premiumSlotBarPosition = "";
        public string genericFeatureBarPosition = "";
        public string categoryBarPosition = "";
        public int minimapScaleFactor = 0;
        public string proActionBarLayout = "";
        public string gameFeatureBarLayout = "";
        public string gameFeatureBarPosition = "";
        public string name_124 = "";
        public string standardSlotBarPosition = "";

        public SaveBarsSettingsRequest(int param1 = 0, string param2 = "", string param3 = "", string param4 = "", string param5 = "", string param6 = "", string param7 = "", string param8 = "", string param9 = "", string param10 = "", string param11 = "", string param12 = "", string param13 = "", string param14 = "", bool param15 = false) {
            this.minimapScaleFactor = param1;
            this.categoryBarPosition = param2;
            this.barState = param3;
            this.genericFeatureBarPosition = param4;
            this.genericFeatureBarLayout = param5;
            this.gameFeatureBarPosition = param6;
            this.gameFeatureBarLayout = param7;
            this.standardSlotBarPosition = param8;
            this.standardSlotBarLayout = param9;
            this.premiumSlotBarPosition = param10;
            this.premiumSlotBarLayout = param11;
            this.proActionBarPosition = param12;
            this.proActionBarLayout = param13;
            this.name_124 = param14;
            this.var_505 = param15;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.standardSlotBarLayout = param1.ReadUTF();
            this.genericFeatureBarLayout = param1.ReadUTF();
            this.barState = param1.ReadUTF();
            this.var_505 = param1.ReadBoolean();
            this.proActionBarPosition = param1.ReadUTF();
            this.premiumSlotBarLayout = param1.ReadUTF();
            this.premiumSlotBarPosition = param1.ReadUTF();
            this.genericFeatureBarPosition = param1.ReadUTF();
            this.categoryBarPosition = param1.ReadUTF();
            this.minimapScaleFactor = param1.ReadInt();
            this.minimapScaleFactor = param1.Shift(this.minimapScaleFactor, 17);
            this.proActionBarLayout = param1.ReadUTF();
            this.gameFeatureBarLayout = param1.ReadUTF();
            this.gameFeatureBarPosition = param1.ReadUTF();
            this.name_124 = param1.ReadUTF();
            this.standardSlotBarPosition = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.standardSlotBarLayout);
            param1.WriteUTF(this.genericFeatureBarLayout);
            param1.WriteUTF(this.barState);
            param1.WriteBoolean(this.var_505);
            param1.WriteUTF(this.proActionBarPosition);
            param1.WriteUTF(this.premiumSlotBarLayout);
            param1.WriteUTF(this.premiumSlotBarPosition);
            param1.WriteUTF(this.genericFeatureBarPosition);
            param1.WriteUTF(this.categoryBarPosition);
            param1.WriteInt(param1.Shift(this.minimapScaleFactor, 15));
            param1.WriteUTF(this.proActionBarLayout);
            param1.WriteUTF(this.gameFeatureBarLayout);
            param1.WriteUTF(this.gameFeatureBarPosition);
            param1.WriteUTF(this.name_124);
            param1.WriteUTF(this.standardSlotBarPosition);
        }
    }
}
