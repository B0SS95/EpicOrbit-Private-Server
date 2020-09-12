namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol {
    public static class APlayCodec {

        public const byte MSGTYPE_LOWLEVEL_INTRODUCTION = 0x10;
        public const byte MSGTYPE_LOWLEVEL_PING = 0x11;
        public const byte MSGTYPE_LOWLEVEL_PONG = 0x12;
        public const byte MSGTYPE_PAYLOAD = 0x50;
        public const byte MSGTYPE_PAYLOAD_GZIP = 0x51;
        public const byte MSGTYPE_PAYLOAD_BINARY = 0x52;
        public const byte MSGTYPE_PAYLOAD_BINARY_JSON = 0x53;

    }
}
