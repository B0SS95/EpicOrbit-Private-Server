using EpicOrbit.Emulator.Chat.Infinicast.Helper;
using EpicOrbit.Emulator.Chat.Infinicast.Messages;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces;

namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol {
    public static class APlayProtocolDecoder {

        public static object Decode(IBinaryInputStream data) {
            byte type = data.ReadByte();

            switch (type) {
                case 42:
                    return new LowLevelConnectMessage();

                case APlayCodec.MSGTYPE_PAYLOAD:
                    APlayStringMessage msg = new APlayStringMessage();
                    msg.SetDataAsString(data.ReadString());
                    return msg;

                case APlayCodec.MSGTYPE_PAYLOAD_BINARY_JSON:
                    GameContext.Logger.LogDebug($"Package [MSGTYPE_PAYLOAD_BINARY_JSON] received: {data.ReadInt()} byte(s)");

                    APlayStringMessage msg2 = new APlayStringMessage();
                    msg2.SetDataAsJson(APlayBinaryMessage.ReadJsonEncoded(data));
                    return msg2;

                case APlayCodec.MSGTYPE_PAYLOAD_GZIP: // this even used?
                    APlayStringMessage msg3 = new APlayStringMessage();
                    msg3.SetDataAsString(StringCompressor.Decompress(data.ReadBytes()));
                    return msg3;

                case APlayCodec.MSGTYPE_LOWLEVEL_PING:
                    GameContext.Logger.LogDebug($"Package [MSGTYPE_LOWLEVEL_PING] received: {data.ReadInt()} byte(s)");

                    return new LowLevelPingMessage {
                        PingTime = data.ReadLong(),
                        LastRTT = data.ReadInt()
                    };
                case APlayCodec.MSGTYPE_LOWLEVEL_PONG:
                    GameContext.Logger.LogDebug($"Package [MSGTYPE_LOWLEVEL_PONG] received: {data.ReadInt()} byte(s)");

                    return new LowLevelPongMessage {
                        PingTime = data.ReadLong()
                    };
                case APlayCodec.MSGTYPE_LOWLEVEL_INTRODUCTION:
                    return new LowLevelIntroductionMessage {
                        AddressString = data.ReadString()
                    };
            }
            return null;
        }

    }
}
