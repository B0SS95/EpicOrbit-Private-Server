using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cake.Figlet;
using EpicOrbit.Emulator;
using EpicOrbit.Shared.Implementations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Conventions;
using Nito.AsyncEx;

namespace EpicOrbit.Server {
    public class Program {

        private static ConsoleLogger _logger = new ConsoleLogger(LogLevel.Debug);

        public static void Main(string[] args) {
            AsyncContext.Run(() => MainAsync(args));
        }

        private static async Task MainAsync(string[] args) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(FigletAliases.Figlet(null, " EpicOrbit"));
            Console.ResetColor();

            ConventionRegistry.Register("ignore.null", new ConventionPack {
                      new IgnoreExtraElementsConvention(true),
                      new IgnoreIfNullConvention(true)
                  }, t => true);

            var logger = new ConsoleLogger(LogLevel.Debug);
            AppDomain.CurrentDomain.GetAssemblies().Where(y => y.FullName.Contains("EpicOrbit")).ToList()
                .ForEach(x => logger.LogDebug(x.FullName));

            GameContext.Initialize(logger, "mongodb://127.0.0.1", "epicorbit",
                new IPEndPoint(IPAddress.Any, 8080), new IPEndPoint(IPAddress.Any, 843),
                new IPEndPoint(IPAddress.Any, 9338));

            GameContext.Start();

            await CreateWebHostBuilder(args).RunAsync();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(new ConfigurationBuilder()
                        .AddCommandLine(args)
                        .Build()
                    )
                    .ConfigureLogging((context, logging) => {
                        logging.ClearProviders();
                        logging.AddProvider(new LoggerProvider(_logger));
                    })
                    .UseUrls("http://0.0.0.0:80")
                    .UseStartup<Startup>()
                    .UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                    .Build();
        }

    }
}
