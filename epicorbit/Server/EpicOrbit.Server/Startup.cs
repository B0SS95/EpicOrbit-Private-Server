using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Middlewares.Authentication;
using EpicOrbit.Server.Middlewares.Authorization;
using EpicOrbit.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EpicOrbit.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAuthentication("tokenized")
                .AddScheme<AuthenticationSchemeOptions, TokenizedAuthHandler>("tokenized", _ => { });

            services.AddAuthorization(o => {
                foreach (GlobalRole role in Enum.GetValues(typeof(GlobalRole))) {
                    o.AddPolicy(role.ToString("G"), policy => policy.Requirements.Add(new RoleRequired(role)));
                }
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton(new RessourceProviderManager(Configuration.GetValue<string>("Providers:Token")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
