using EpicOrbit.Emulator.Chat.Infinicast.Messages;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Implementations;

namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol {
    public static class APlayProtocolEncoder {

        public static byte[] Encode(object data) {
            BinaryOutputStream stream = new BinaryOutputStream();
            switch (data) {
                case LowLevelConnectMessage lowLevelConnectMessage: // no length prefix
                    stream.WriteByte(42); 
                    stream.WriteByte(1);
                    break;

                case APlayStringMessage aPlayStringMessage:
                    stream.WriteByte(APlayCodec.MSGTYPE_PAYLOAD_BINARY_JSON);

                    BinaryOutputStream content = new BinaryOutputStream();
                    content.WriteJsonEncoded(aPlayStringMessage.GetDataAsDecodedJson());

                    stream.WriteBytes(content.GetData());
                    break;

                case LowLevelIntroductionMessage lowLevelIntroductionMessage: // no length prefix
                    stream.WriteByte(APlayCodec.MSGTYPE_LOWLEVEL_INTRODUCTION);
                    stream.WriteString(lowLevelIntroductionMessage.AddressString);
                    break;

                case LowLevelPingMessage lowLevelPingMessage:
                    stream.WriteByte(APlayCodec.MSGTYPE_LOWLEVEL_PING);

                    stream.WriteInt(12); // 8 + 4
                    stream.WriteLong(lowLevelPingMessage.PingTime);
                    stream.WriteInt(lowLevelPingMessage.LastRTT);
                    break;

                case LowLevelPongMessage lowLevelPongMessage:
                    stream.WriteByte(APlayCodec.MSGTYPE_LOWLEVEL_PONG);

                    stream.WriteInt(8);
                    stream.WriteLong(lowLevelPongMessage.PingTime);
                    break;
            }

            return stream.GetData();
        }

    }
}
