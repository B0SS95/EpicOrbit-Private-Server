using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class KillScreenOptionModule : ICommand {

        public short ID { get; set; } = 24502;
        public MessageLocalizedWildcardCommand descriptionKey;
        public int cooldownTime = 0;
        public MessageLocalizedWildcardCommand descriptionKeyAddon;
        public MessageLocalizedWildcardCommand buttonKey;
        public KillScreenOptionTypeModule repairType;
        public MessageLocalizedWildcardCommand toolTipKey;
        public PriceModule price;
        public bool affordableForPlayer = false;

        public KillScreenOptionModule(KillScreenOptionTypeModule param1 = null, PriceModule param2 = null, bool param3 = false, int param4 = 0, MessageLocalizedWildcardCommand param5 = null, MessageLocalizedWildcardCommand param6 = null, MessageLocalizedWildcardCommand param7 = null, MessageLocalizedWildcardCommand param8 = null) {
            if (param1 == null) {
                this.repairType = new KillScreenOptionTypeModule();
            } else {
                this.repairType = param1;
            }
            if (param2 == null) {
                this.price = new PriceModule();
            } else {
                this.price = param2;
            }
            this.affordableForPlayer = param3;
            this.cooldownTime = param4;
            if (param5 == null) {
                this.descriptionKey = new MessageLocalizedWildcardCommand();
            } else {
                this.descriptionKey = param5;
            }
            if (param6 == null) {
                this.descriptionKeyAddon = new MessageLocalizedWildcardCommand();
            } else {
                this.descriptionKeyAddon = param6;
            }
            if (param7 == null) {
                this.toolTipKey = new MessageLocalizedWildcardCommand();
            } else {
                this.toolTipKey = param7;
            }
            if (param8 == null) {
                this.buttonKey = new MessageLocalizedWildcardCommand();
            } else {
                this.buttonKey = param8;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.descriptionKey = lookup.Lookup(param1) as MessageLocalizedWildcardCommand;
            this.descriptionKey.Read(param1, lookup);
            this.cooldownTime = param1.ReadInt();
            this.cooldownTime = param1.Shift(this.cooldownTime, 25);
            this.descriptionKeyAddon = lookup.Lookup(param1) as MessageLocalizedWildcardCommand;
            this.descriptionKeyAddon.Read(param1, lookup);
            this.buttonKey = lookup.Lookup(param1) as MessageLocalizedWildcardCommand;
            this.buttonKey.Read(param1, lookup);
            this.repairType = lookup.Lookup(param1) as KillScreenOptionTypeModule;
            this.repairType.Read(param1, lookup);
            param1.ReadShort();
            this.toolTipKey = lookup.Lookup(param1) as MessageLocalizedWildcardCommand;
            this.toolTipKey.Read(param1, lookup);
            this.price = lookup.Lookup(param1) as PriceModule;
            this.price.Read(param1, lookup);
            this.affordableForPlayer = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.descriptionKey.Write(param1);
            param1.WriteInt(param1.Shift(this.cooldownTime, 7));
            this.descriptionKeyAddon.Write(param1);
            this.buttonKey.Write(param1);
            this.repairType.Write(param1);
            param1.WriteShort(5154);
            this.toolTipKey.Write(param1);
            this.price.Write(param1);
            param1.WriteBoolean(this.affordableForPlayer);
        }
    }
}
