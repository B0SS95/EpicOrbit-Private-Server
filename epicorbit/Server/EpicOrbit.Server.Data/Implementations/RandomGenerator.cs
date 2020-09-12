using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicOrbit.Server.Data.Implementations {
    public static class RandomGenerator {

        private static Random random = new Random();
        public static string String(int length) {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static byte[] Bytes(int length) {
            byte[] result = new byte[length];
            random.NextBytes(result);
            return result;
        }

        public static int UniqueIdentifier() {
            return random.Next(1, 100000000);
        }

        private static int ID_RANGE_MIN = -1000000;
        private static int ID_RANGE_MAX = 0;
        private static int ID_RANGE_CURRENT = ID_RANGE_MIN;
        private static Queue<int> _deleted = new Queue<int>();

        public static int Identifier() {
            lock (_deleted) {
                if (_deleted.Count > 0) {
                    return _deleted.Dequeue();
                }

                if (ID_RANGE_CURRENT >= ID_RANGE_MAX) {
                    throw new Exception("no more id's available");
                }

                return ID_RANGE_CURRENT++;
            }
        }

        public static void Remove(int id) {
            lock (_deleted) {
                _deleted.Enqueue(id);
            }
        }

    }
}
