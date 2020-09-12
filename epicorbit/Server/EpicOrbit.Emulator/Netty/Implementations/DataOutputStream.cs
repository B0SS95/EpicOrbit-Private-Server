using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Emulator.Netty.Implementations {
    internal class DataOutputStream : Shifter, IDataOutput {

        private List<byte> _data = new List<byte>();

        public byte[] GetData() {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes((short)_data.Count));
            bytes.Reverse();
            bytes.AddRange(_data);
            return bytes.ToArray();
        }

        public byte[] GetDataRaw() {
            return _data.ToArray();
        }

        public void WriteBoolean(bool value) {
            _data.Add(Convert.ToByte(value));
        }

        public void WriteByte(byte value) {
            _data.Add(value);
        }

        public void WriteBytes(byte[] value) {
            WriteInt(value.Length);
            _data.AddRange(value);
        }

        public void WriteDouble(double value) {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            _data.AddRange(data);
        }

        public void WriteLong(long value) {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            _data.AddRange(data);
        }

        public void WriteFloat(float value) {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            _data.AddRange(data);
        }

        public void WriteInt(int value) {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            _data.AddRange(data);
        }

        public void WriteShort(short value) {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            _data.AddRange(data);
        }

        public void WriteUTF(string value) {
            if (value == null) {
                value = "";
            }

            byte[] stringBytes = Encoding.UTF8.GetBytes(value + (char)0x00);
            WriteShort((short)stringBytes.Length);
            _data.AddRange(stringBytes);
        }

    }
}
