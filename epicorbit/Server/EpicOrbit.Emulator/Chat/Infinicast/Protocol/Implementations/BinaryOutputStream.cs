using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol.Implementations {
    public class BinaryOutputStream : IBinaryOutputStream {

        private List<byte> _data = new List<byte>();

        public byte[] GetData() {
            return _data.ToArray();
        }

        public void WriteBool(bool value) {
            _data.Add((byte)(value ? 1 : 0));
        }

        public void WriteByte(byte value) {
            _data.Add(value);
        }

        public void WriteBytes(int prefixLength, byte[] value) {
            switch (prefixLength) {
                case 1:
                    WriteByte((byte)value.Length);
                    break;
                case 2:
                    WriteShort((short)value.Length);
                    break;
                case 4:
                    WriteInt(value.Length);
                    break;
                default: throw new ArgumentException(nameof(prefixLength));
            }

            _data.AddRange(value);
        }

        public void WriteBytes(byte[] value) {
            WriteBytes(4, value);
        }

        public void WriteDouble(double value) {
            _data.AddRange(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteFloat(float value) {
            _data.AddRange(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteInt(int value) {
            _data.AddRange(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteLong(long value) {
            _data.AddRange(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteShort(short value) {
            _data.AddRange(BitConverter.GetBytes(value).Reverse());
        }

        public void WriteString(int prefixLength, string value) {
            WriteBytes(prefixLength, Encoding.UTF8.GetBytes(value));
        }

        public void WriteString(string value) {
            WriteString(4, value);
        }
    }
}
