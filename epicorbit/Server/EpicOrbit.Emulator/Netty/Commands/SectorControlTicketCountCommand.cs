using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlTicketCountCommand : ICommand {

        public short ID { get; set; } = 15502;
        public int ticketCount = 0;
        public int maxTickets = 0;
        public FactionModule faction;

        public SectorControlTicketCountCommand(FactionModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param1;
            }
            this.ticketCount = param2;
            this.maxTickets = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ticketCount = param1.ReadInt();
            this.ticketCount = param1.Shift(this.ticketCount, 16);
            param1.ReadShort();
            this.maxTickets = param1.ReadInt();
            this.maxTickets = param1.Shift(this.maxTickets, 22);
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.ticketCount, 16));
            param1.WriteShort(25470);
            param1.WriteInt(param1.Shift(this.maxTickets, 10));
            this.faction.Write(param1);
        }
    }
}
