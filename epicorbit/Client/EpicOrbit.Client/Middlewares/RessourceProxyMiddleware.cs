using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Services;
using Microsoft.AspNetCore.Http;

namespace EpicOrbit.Client.Middlewares {
    public class RessourcesProxyMiddleware {

        private readonly RequestDelegate _next;
        private readonly RessourcesProxy _ressourcesProxy;

        public RessourcesProxyMiddleware(RequestDelegate next, RessourcesProxy ressourcesProxy) {
            _next = next;
            _ressourcesProxy = ressourcesProxy;
        }

        public async Task InvokeAsync(HttpContext context) {
            if (Path.HasExtension(context.Request.Path)) {

                if (context.Request.Path.ToString().Substring(1).StartsWith("_")
                    || context.Request.Path.ToString().Substring(1) == "favicon.ico"
                    || File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", context.Request.Path.ToString().Substring(1)))) {
                    goto Finish;
                }

                string ressourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "game", context.Request.Path.ToString().Substring(1));
                if (!File.Exists(ressourcePath)) {
                    try {
                        using (MemoryStream input = new MemoryStream()) {
                            await _ressourcesProxy.Retrieve(context.Request.Path.ToString().Substring(1), input);
                            input.Position = 0;

                            if (input.Length > 0) {
                                Directory.CreateDirectory(Path.GetDirectoryName(ressourcePath));
                                using (Stream output = File.Open(ressourcePath, FileMode.CreateNew)) {
                                    await input.CopyToAsync(output);
                                }
                            }
                        }
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }

        Finish: await _next(context);
        }

    }
}
