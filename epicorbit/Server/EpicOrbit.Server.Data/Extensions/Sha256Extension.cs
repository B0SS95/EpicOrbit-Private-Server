using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EpicOrbit.Server.Data.Extensions {
    public static class Sha256Extension {

        private static readonly SHA256 sha256 = SHA256.Create();
        public static byte[] ComputeHash(this string password, byte[] salt) {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password).Merge(salt));
        }

    }
}
