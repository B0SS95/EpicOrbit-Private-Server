using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using EpicOrbit.Shared.Extensions;
using EpicOrbit.Shared.ViewModels.Ressources;
using Newtonsoft.Json;

namespace EpicOrbit.Client.Packer {
    class Program {

        static string ByteArrayToString(byte[] ba) {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        static Stream FileCompressed(string path) {
            MemoryStream output = new MemoryStream();
            using (Stream input = File.OpenRead(path))
            using (GZipStream compressor = new GZipStream(output, CompressionLevel.Optimal, true)) {
                input.CopyTo(compressor);
            }

            output.Position = 0;
            return output;
        }

        static void Encrypt(string token, string source, string destination) {
            RessourceTableView tableView = new RessourceTableView {
                Table = new Dictionary<string, RessourceTableItemView>(),
                TokenHash = token.Hash()
            };

            int counter = 1;
            Stream container = null;

            foreach (string path in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories)) {
                string encryptedPath = ByteArrayToString(token.Hash(Path.GetRelativePath(source, path).Replace(@"\", "/")));
                Console.WriteLine("{0}  --  {1}", Path.GetRelativePath(source, path).Replace(@"\", "/"), encryptedPath);

                using (MemoryStream output = new MemoryStream())
                using (Stream input = FileCompressed(path)) { //File.Open(path, FileMode.Open, FileAccess.Read)) {
                    SecurityExtension.EncryptAES(input, output, token);

                    if (container == null || container.Length + output.Length > (50 * 1024 * 1024)) {
                        container?.Dispose();
                        container = File.Create(Path.Combine(destination, $"0000000000000000-{++counter}"));
                        Console.WriteLine("Current Archive: {0}", counter);
                    }

                    output.Position = 0;
                    tableView.Table.Add(encryptedPath, new RessourceTableItemView {
                        Container = counter,
                        Length = output.Length,
                        Offset = container.Position
                    });

                    output.CopyTo(container);
                }
            }

            container?.Dispose();

            using (Stream output = File.Create(Path.Combine(destination, "0000000000000000-1")))
            using (MemoryStream input = new MemoryStream(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tableView)))) {
                SecurityExtension.EncryptAES(input, output, token);
            }

            using (Stream output = File.Create(Path.Combine(destination, "0000000000000000-0")))
            using (MemoryStream input = new MemoryStream(token.Hash())) {
                input.CopyTo(output);
            }
        }


        static void AppendEncrypt(string token, string source, string destination) {
            RessourceTableView tableView = null;
            using (Stream input = File.OpenRead(Path.Combine(destination, "0000000000000000-1")))
            using (MemoryStream output = new MemoryStream()) {
                SecurityExtension.DecryptAES(input, output, token);
                output.Position = 0;
                tableView = JsonConvert.DeserializeObject<RessourceTableView>(Encoding.UTF8.GetString(output.ToArray()));
            }

            int counter = tableView.Table.Values.Max(x => x.Container) - 1;
            Stream container = null;

            foreach (string path in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories)) {
                string encryptedPath = ByteArrayToString(token.Hash(Path.GetRelativePath(source, path).Replace(@"\", "/")));
                if (tableView.Table.ContainsKey(encryptedPath)) {
                    continue;
                }
                Console.WriteLine("{0}  --  {1}", Path.GetRelativePath(source, path).Replace(@"\", "/"), encryptedPath);

                using (MemoryStream output = new MemoryStream())
                using (Stream input = FileCompressed(path)) { //File.Open(path, FileMode.Open, FileAccess.Read)) {
                    SecurityExtension.EncryptAES(input, output, token);

                    if (container == null || container.Length + output.Length > (50 * 1024 * 1024)) {
                        container?.Dispose();
                        container = File.Open(Path.Combine(destination, $"0000000000000000-{++counter}"), FileMode.OpenOrCreate);
                        container.Seek(0, SeekOrigin.End); // seek end

                        Console.WriteLine("Current Archive: {0}", counter);
                    }

                    output.Position = 0;
                    tableView.Table.Add(encryptedPath, new RessourceTableItemView {
                        Container = counter,
                        Length = output.Length,
                        Offset = container.Position
                    });

                    output.CopyTo(container);
                }
            }

            container?.Dispose();

            using (Stream output = File.Create(Path.Combine(destination, "0000000000000000-1")))
            using (MemoryStream input = new MemoryStream(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tableView)))) {
                SecurityExtension.EncryptAES(input, output, token);
            }
        }

        static bool CheckExists(string token, string destination) {
            if (!File.Exists(Path.Combine(destination, "0000000000000000-0"))
                || !File.ReadAllBytes(Path.Combine(destination, "0000000000000000-0")).SequenceEqual(token.Hash())) {
                return false;
            }

            try {
                if (File.Exists(Path.Combine(destination, "0000000000000000-1"))) {
                    using (Stream input = File.OpenRead(Path.Combine(destination, "0000000000000000-1")))
                    using (MemoryStream output = new MemoryStream()) {
                        SecurityExtension.DecryptAES(input, output, token);
                        output.Position = 0;

                        var result = JsonConvert.DeserializeObject<RessourceTableView>(Encoding.UTF8.GetString(output.ToArray()));
                        return result.TokenHash.SequenceEqual(token.Hash());
                    }
                }
            } catch { }

            return false;
        }

        static void Main(string[] args) {
            Console.WriteLine("### EpicOrbit Packer");

            Console.Write("### Input Token: ");
            string token = Console.ReadLine();
            if (token == null || string.IsNullOrWhiteSpace(token)) {
                token = SecurityExtension.GenerateToken();
                Console.WriteLine("### Token: '{0}'", token);
            }

            Console.Write("### Input Folder: ");
            string input = Console.ReadLine();

            Console.Write("### Output Folder: ");
            string output = Console.ReadLine();

            if (CheckExists(token, output)) {
                Console.WriteLine("### Output already a package, appending existing!");
                AppendEncrypt(token, input, output);
            } else {
                Console.WriteLine("### Output is empty, creating new package!");
                if (Directory.Exists(output)) {
                    Directory.Delete(output, true);
                }
                Directory.CreateDirectory(output);
                Encrypt(token, input, output);
            }

            Console.WriteLine("### Done!");

            Console.WriteLine("### Token: '{0}'", token);
            Console.ReadLine();
        }
    }
}
