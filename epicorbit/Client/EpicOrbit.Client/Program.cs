using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Shared.Implementations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;

namespace EpicOrbit.Client {
    public class Program {

        public static ConsoleLogger _logger = new ConsoleLogger(LogLevel.Debug);

        public static void Main(string[] args) {
            AsyncContext.Run(() => MainAsync(args));
        }

        private static async Task MainAsync(string[] args) {
            var logger = new ConsoleLogger(LogLevel.Debug);
            AppDomain.CurrentDomain.GetAssemblies().Where(y => y.FullName.Contains("EpicOrbit")).ToList()
                .ForEach(x => logger.LogDebug(x.FullName));

            ClientContext.Initialize(_logger, "127.0.0.1");

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
                    .UseUrls("http://0.0.0.0:49679")
                    .UseStartup<Startup>()
                    .UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                    .Build();
        }
    }
}
