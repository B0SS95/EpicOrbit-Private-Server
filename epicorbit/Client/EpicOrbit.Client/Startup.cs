using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using EpicOrbit.Client.Services;
using EpicOrbit.Client.Middlewares.Extensions;

namespace EpicOrbit.Client {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDataProtection();
            services.AddSingleton<RessourcesProxy>();

            services.AddScoped<ApiClient>();
            services.AddScoped<AccountService>();
            services.AddScoped<HangarService>();
            services.AddScoped<ClanService>();

            services.AddScoped<NotificationService>();
            services.AddScoped<ComponentService>();
            services.AddScoped<StateService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRessourceProxy();
            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=43200"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "game")),
                ServeUnknownFileTypes = true
            });

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
