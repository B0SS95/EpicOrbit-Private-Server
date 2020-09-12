using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Shared.Extensions {
    public static class SecurityExtension {

        private static Random _random = new Random();
        private static SHA256CryptoServiceProvider _sha256 = new SHA256CryptoServiceProvider();
        private static SHA1CryptoServiceProvider _sha1 = new SHA1CryptoServiceProvider();

        public static bool Verify(string original, string signed, string publicKey) {
            using (RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider()) {
                _rsa.ImportCspBlob(DecodeKey(publicKey));
                return _rsa.VerifyData(Encoding.UTF8.GetBytes(original), _sha1, Convert.FromBase64String(signed));
            }
        }

        public static string Sign(string data, string privateKey) {
            using (RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider()) {
                _rsa.ImportCspBlob(DecodeKey(privateKey));
                return Convert.ToBase64String(_rsa.SignData(Encoding.UTF8.GetBytes(data), _sha1));
            }
        }

        public static string Encrypt(string data, string publicKey) {
            using (RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider()) {
                _rsa.ImportCspBlob(DecodeKey(publicKey));
                return Convert.ToBase64String(_rsa.Encrypt(Encoding.UTF8.GetBytes(data), false));
            }
        }

        public static string Decrypt(string encrypted, string privateKey) {
            using (RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider()) {
                _rsa.ImportCspBlob(DecodeKey(privateKey));
                return Encoding.UTF8.GetString(_rsa.Decrypt(Convert.FromBase64String(encrypted), false));
            }
        }

        public static string String(int length) {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string GenerateToken() {
            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(String(32) + "|" + String(16))
            );
        }

        private static void RetrieveFromToken(string token, out byte[] key, out byte[] iv) {
            key = null;
            iv = null;

            string[] parts = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split('|');
            if (parts.Length == 2) {
                key = Encoding.UTF8.GetBytes(parts[0]);
                iv = Encoding.UTF8.GetBytes(parts[1]);
            }
        }

        public static void EncryptAES(Stream data, Stream target, string token) {
            RetrieveFromToken(token, out byte[] key, out byte[] iv);

            using (RijndaelManaged _aes = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, _aes.CreateEncryptor(key, iv), CryptoStreamMode.Write)) {
                data.CopyTo(cryptoStream);
                cryptoStream.FlushFinalBlock();

                memoryStream.Position = 0;
                memoryStream.CopyTo(target);
            }
        }

        public static void DecryptAES(Stream data, Stream target, string token) {
            RetrieveFromToken(token, out byte[] key, out byte[] iv);

            using (RijndaelManaged _aes = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            using (CryptoStream cs = new CryptoStream(data, _aes.CreateDecryptor(key, iv), CryptoStreamMode.Read)) {
                cs.CopyTo(target);
            }
        }

        public static Tuple<string, string> CreatePair() {
            using (var _rsa = new RSACryptoServiceProvider(1024)) {
                try {
                    return new Tuple<string, string>(EncodeKey(_rsa.ExportCspBlob(true)), EncodeKey(_rsa.ExportCspBlob(false)));
                } finally {
                    _rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string EncodeKey(byte[] key) {
            return Convert.ToBase64String(key);
        }

        public static byte[] DecodeKey(string key) {
            return Convert.FromBase64String(key);
        }

        public static byte[] Hash(this string data, string salt) {
            return _sha256.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
        }

        public static byte[] Hash(this string data) {
            return _sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        public static byte[] ComputeHash(this Stream stream) {
            return _sha1.ComputeHash(stream);
        }

    }
}
