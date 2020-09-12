using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Client.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EpicOrbit.Client.Middlewares.Extensions {
    public static class RessourcesProxyMiddlewareExtensions {

        public static IApplicationBuilder UseRessourceProxy(this IApplicationBuilder builder) {
            return builder.UseMiddleware<RessourcesProxyMiddleware>();
        }

    }
}
