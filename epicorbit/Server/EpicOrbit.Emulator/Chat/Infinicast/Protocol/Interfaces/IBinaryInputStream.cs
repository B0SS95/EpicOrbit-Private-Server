namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces {
    public interface IBinaryInputStream {

        byte ReadByte();
        short ReadShort();
        int ReadInt();
        bool ReadBool();
        double ReadDouble();
        float ReadFloat();
        long ReadLong();
        string ReadString(int prefixLength);
        string ReadString();
        byte[] ReadBytes(int prefixLength);
        byte[] ReadBytes();

    }
}
