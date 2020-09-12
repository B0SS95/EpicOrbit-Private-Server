namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface IDataOutput : IShiftable {

        void WriteShort(short value);
        void WriteInt(int value);
        void WriteFloat(float value);
        void WriteLong(long value);
        void WriteBoolean(bool value);
        void WriteBytes(byte[] value);
        void WriteUTF(string value);
        void WriteDouble(double value);
        void WriteByte(byte value);

    }
}
