namespace EpicOrbit.Emulator.Netty.Interfaces {
    public interface IDataInput : IShiftable {

        short ReadShort();
        int ReadInt();
        float ReadFloat();
        bool ReadBoolean();
        byte[] ReadBytes();
        string ReadUTF();
        double ReadDouble();
        byte ReadByte();
        long ReadLong();

    }
}
