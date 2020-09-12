using EpicOrbit.Emulator.Chat.Infinicast.Protocol.Interfaces;
using Newtonsoft.Json.Linq;
using System.Text;

namespace EpicOrbit.Emulator.Chat.Infinicast.Protocol {
    public static class APlayBinaryMessage {

        #region {[ HELPER ]}
        private static void WriteTypeEncoded(this IBinaryOutputStream stream, JToken value) {
            if (value == null) {
                stream.WriteByte(0);
            }

            switch (value.Type) {
                case JTokenType.Boolean:
                    stream.WriteByte(1);
                    stream.WriteBooleanEncoded((bool?)value);
                    break;
                case JTokenType.Array:
                    stream.WriteByte(2);
                    stream.WriteArrayEncoded((JArray)value);
                    break;
                case JTokenType.Integer: // + byte
                    stream.WriteByte(3);
                    stream.WriteIntEncoded((int?)value);
                    break;
                case JTokenType.Object:
                    stream.WriteByte(4);
                    stream.WriteJsonEncoded((JObject)value);
                    break;
                case JTokenType.String:
                    stream.WriteByte(5);
                    stream.WriteStringEncoded((string)value);
                    break;
                case JTokenType.Float: // + double und long
                    stream.WriteByte(8);
                    stream.WriteFloatEncoded((float)value);
                    break;

            }
        }

        private static JToken ReadTypeEncoded(this IBinaryInputStream stream) {
            switch (stream.ReadByte()) {
                case 1:
                    return stream.ReadBooleanEncoded();
                case 2:
                    return stream.ReadArrayEncoded();
                case 3:
                    return stream.ReadIntEncoded();
                case 4:
                    return stream.ReadJsonEncoded();
                case 5:
                    return stream.ReadStringEncoded();
                case 6:
                    return stream.ReadByte();
                case 7:
                    return stream.ReadLongEncoded();
                case 8:
                    return stream.ReadFloatEncoded();
                case 9:
                    return stream.ReadDoubleEncoded();
            }
            return null;
        }
        #endregion

        #region {[ WRITE ]}
        public static void WriteIntEncoded(this IBinaryOutputStream stream, int? value) {
            if (value == null) {
                stream.WriteInt(int.MinValue);
            } else {
                stream.WriteInt(value.Value);
            }
        }
        public static void WriteLongEncoded(this IBinaryOutputStream stream, long? value) {
            if (value == null) {
                stream.WriteLong(int.MinValue);
            } else {
                stream.WriteLong(value.Value);
            }
        }

        public static void WriteFloatEncoded(this IBinaryOutputStream stream, float? value) {
            if (value == null) {
                stream.WriteFloat(int.MinValue);
            } else {
                stream.WriteFloat(value.Value);
            }
        }

        public static void WriteDoubleEncoded(this IBinaryOutputStream stream, double? value) {
            if (value == null) {
                stream.WriteDouble(int.MinValue);
            } else {
                stream.WriteDouble(value.Value);
            }
        }

        public static void WriteStringEncoded(this IBinaryOutputStream stream, string value) {
            if (value == null) {
                stream.WriteInt(0);
            } else {
                stream.WriteBytes(Encoding.UTF8.GetBytes(value));
            }
        }

        public static void WriteBooleanEncoded(this IBinaryOutputStream stream, bool? value) {
            if (value == null) {
                stream.WriteByte(0);
            } else {
                stream.WriteByte((byte)(value.Value ? 2 : 1));
            }
        }

        public static void WriteArrayEncoded(this IBinaryOutputStream stream, JArray value) {
            if (value == null) {
                stream.WriteInt(-1);
            } else {
                stream.WriteInt(value.Count);
                foreach (var item in value) {
                    WriteTypeEncoded(stream, item);
                }
            }
        }

        public static void WriteJsonEncoded(this IBinaryOutputStream stream, JObject keyValues) {
            if (keyValues == null) {
                stream.WriteInt(-1);
            } else {
                stream.WriteInt(keyValues.Count);
                foreach (var item in keyValues) {
                    stream.WriteStringEncoded(item.Key);
                    stream.WriteTypeEncoded(item.Value);
                }
            }
        }
        #endregion

        #region {[ READ ]}
        public static bool? ReadBooleanEncoded(this IBinaryInputStream stream) {
            byte value = stream.ReadByte();
            if (value == 0) {
                return null;
            }

            return value == 2;
        }

        public static int? ReadIntEncoded(this IBinaryInputStream stream) {
            int value = stream.ReadInt();
            if (value == int.MinValue) {
                return null;
            }

            return value;
        }

        public static long? ReadLongEncoded(this IBinaryInputStream stream) {
            long value = stream.ReadLong();
            if (value == int.MinValue) {
                return null;
            }

            return value;
        }

        public static float? ReadFloatEncoded(this IBinaryInputStream stream) {
            float value = stream.ReadFloat();
            if (value == int.MinValue) {
                return null;
            }

            return value;
        }

        public static double? ReadDoubleEncoded(this IBinaryInputStream stream) {
            double value = stream.ReadDouble();
            if (value == int.MinValue) {
                return null;
            }

            return value;
        }

        public static string ReadStringEncoded(this IBinaryInputStream stream) {
            byte[] value = stream.ReadBytes();
            if (value.Length <= 0) {
                return null;
            }

            return Encoding.UTF8.GetString(value);
        }

        public static JArray ReadArrayEncoded(this IBinaryInputStream stream) {
            int size = stream.ReadInt();
            if (size < 0) {
                return null;
            }

            JArray holder = new JArray();
            for (int i = 0; i < size; i++) {
                holder.Add(ReadTypeEncoded(stream));
            }
            return holder;
        }

        public static JObject ReadJsonEncoded(this IBinaryInputStream stream) {
            int size = stream.ReadInt();
            if (size < 0) {
                return null;
            }

            JObject keyValues = new JObject();
            for (int i = 0; i < size; i++) {
                keyValues.Add(stream.ReadStringEncoded(), stream.ReadTypeEncoded());
            }
            return keyValues;
        }
        #endregion

    }
}
