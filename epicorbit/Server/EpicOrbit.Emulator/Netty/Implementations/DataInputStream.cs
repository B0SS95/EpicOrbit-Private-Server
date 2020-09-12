using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Text;

namespace EpicOrbit.Emulator.Netty.Implementations {
    internal class DataInputStream : Shifter, IDataInput {

        private readonly byte[] _data;
        private int _position = 0;

        public DataInputStream(byte[] data) {
            _data = data;
        }

        public bool ReadBoolean() {
            return _data[_position++] == 1;
        }

        public byte ReadByte() {
            return _data[_position++];
        }

        public byte[] ReadBytes() {
            int length = ReadInt();

            byte[] value = _data.SubArray(_position, length);
            _position += length;

            return value;
        }

        public double ReadDouble() {
            double value = BitConverter.ToDouble(new byte[] { _data[_position + 7], _data[_position + 6], _data[_position + 5], _data[_position + 4], _data[_position + 3], _data[_position + 2], _data[_position + 1], _data[_position] }, 0);
            _position += 8;
            return value;
        }

        public float ReadFloat() {
            float value = BitConverter.ToSingle(new byte[] { _data[_position + 3], _data[_position + 2], _data[_position + 1], _data[_position] }, 0);
            _position += 4;
            return value;
        }

        public int ReadInt() {
            int value = BitConverter.ToInt32(new byte[] { _data[_position + 3], _data[_position + 2], _data[_position + 1], _data[_position] }, 0);
            _position += 4;

            return value;
        }

        public long ReadLong() {
            long value = BitConverter.ToInt64(new byte[] { _data[_position + 7], _data[_position + 6], _data[_position + 5], _data[_position + 4], _data[_position + 3], _data[_position + 2], _data[_position + 1], _data[_position] }, 0);
            _position += 8;
            return value;
        }

        public short ReadShort() {
            short value = BitConverter.ToInt16(new byte[] { _data[_position + 1], _data[_position] }, 0);
            _position += 2;

            return value;
        }

        public string ReadUTF() {
            short stringLength = ReadShort();
            string value = Encoding.UTF8.GetString(_data, _position, stringLength);
            _position += stringLength;
            return value;
        }

    }
}
