using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MessageQueueModule : ICommand {

        public short ID { get; set; } = 7998;
        public List<ActivationRequest> messageQueue;

        public MessageQueueModule(List<ActivationRequest> param1 = null) {
            if (param1 == null) {
                this.messageQueue = new List<ActivationRequest>();
            } else {
                this.messageQueue = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.messageQueue.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ActivationRequest;
                tmp_0.Read(param1, lookup);
                this.messageQueue.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.messageQueue.Count);
            foreach (var tmp_0 in this.messageQueue) {
                tmp_0.Write(param1);
            }
        }
    }
}
