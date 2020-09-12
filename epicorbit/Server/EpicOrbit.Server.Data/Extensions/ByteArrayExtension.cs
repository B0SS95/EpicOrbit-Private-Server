using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Extensions {
    public static class ByteArrayExtension {

        public static void IntelligentMerge(this byte[] array1, byte[] array2, int position) {
            if (array1.Length < position + array2.Length) {
                return;
            }

            Array.Copy(array2, 0, array1, position, array2.Length);
        }

        public static byte[] Merge(this byte[] array1, byte[] array2) {
            byte[] newArray = new byte[array1.Length + array2.Length];
            Array.Copy(array1, newArray, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);
            return newArray;
        }

        public static byte[] SubArray(this byte[] array, int position, int length) {
            if (position == 0 && array.Length == length) {
                return array;
            }

            byte[] newArray = new byte[length];
            Array.Copy(array, position, newArray, 0, Math.Min(length, array.Length));
            return newArray;
        }

        public static unsafe bool Is(this byte[] b1, byte[] b2) {
            if (b1 == null && b2 == null) return true;
            if (b1.Length != b2.Length) return false;

            int length = b1.Length;
            fixed (byte* array1 = b1, array2 = b2) {
                byte* ptr1 = array1;
                byte* ptr2 = array2;

                byte a1;
                byte a2;
                while (length-- > 0) {
                    a1 = (*ptr1++);
                    a2 = (*ptr2++);
                    if (a1 != a2) return false;
                }

            }
            return true;
        }

        public static unsafe int CompareTo(this byte[] b1, byte[] b2) {
            if (b1 == null && b2 == null) return 0;
            if (b1.Length > b2.Length) return 1;
            if (b1.Length < b2.Length) return -1;

            int length = b1.Length;
            fixed (byte* array1 = b1, array2 = b2) {
                byte* ptr1 = array1;
                byte* ptr2 = array2;

                byte a1;
                byte a2;
                while (length-- > 0) {
                    a1 = (*ptr1++);
                    a2 = (*ptr2++);
                    if (a1 != a2) return a1.CompareTo(a2);
                }

            }
            return 0;
        }

        public static unsafe int HashCode(this byte[] x) {
            if (x == null || x.Length == 0) return 0;
            int hash = 0;
            int length = x.Length;
            unchecked {
                const int p = 16777619;
                hash = (int)2166136261;
                fixed (byte* array = x) {
                    byte* ptr = array;
                    while (length-- > 0) { hash = (hash ^ (*ptr++)) * p; }
                    hash += hash << 13;
                    hash ^= hash >> 7;
                    hash += hash << 3;
                    hash ^= hash >> 17;
                    hash += hash << 5;
                }
            }
            return hash;
        }

    }
}
