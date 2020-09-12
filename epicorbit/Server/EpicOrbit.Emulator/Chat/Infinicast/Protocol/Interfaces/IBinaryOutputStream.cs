namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces {
    public interface IBinaryOutputStream {

        void WriteByte(byte value);
        void WriteShort(short value);
        void WriteInt(int value);
        void WriteBool(bool value);
        void WriteDouble(double value);
        void WriteFloat(float value);
        void WriteLong(long value);
        void WriteString(int prefixLength, string value);
        void WriteString(string value);
        void WriteBytes(int prefixLength, byte[] value);
        void WriteBytes(byte[] value);


    }
}
