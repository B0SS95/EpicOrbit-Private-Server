using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol.Implementations {
    public class BinaryInputStream : IBinaryInputStream {

        private byte[] _data;
        private int _position;

        public BinaryInputStream(byte[] data) {
            _data = data;
        }

        public bool ReadBool() {
            return BitConverter.ToBoolean(_data, _position++);
        }

        public byte ReadByte() {
            return _data[_position++];
        }

        public byte[] ReadBytes(int prefixLength) {
            int length;
            switch (prefixLength) {
                case 1:
                    length = ReadByte();
                    break;
                case 2:
                    length = ReadShort();
                    break;
                case 4:
                    length = ReadInt();
                    break;
                default: throw new ArgumentException(nameof(prefixLength));
            }

            _position += length;
            return _data.SubArray(_position - length, length);
        }

        public byte[] ReadBytes() {
            return ReadBytes(4);
        }

        public double ReadDouble() {
            return BitConverter.ToDouble(_data.SubArray((_position += 8) - 8, 8).Reverse().ToArray(), 0);
        }

        public float ReadFloat() {
            return BitConverter.ToSingle(_data.SubArray((_position += 4) - 4, 4).Reverse().ToArray(), 0);
        }

        public int ReadInt() {
            return BitConverter.ToInt32(_data.SubArray((_position += 4) - 4, 4).Reverse().ToArray(), 0);
        }

        public long ReadLong() {
            return BitConverter.ToInt64(_data.SubArray((_position += 8) - 8, 8).Reverse().ToArray(), 0);
        }

        public short ReadShort() {
            return BitConverter.ToInt16(_data.SubArray((_position += 2) - 2, 2).Reverse().ToArray(), 0);
        }

        public string ReadString(int prefixLength) {
            return Encoding.UTF8.GetString(ReadBytes(prefixLength));
        }

        public string ReadString() {
            return ReadString(4);
        }

    }
}
