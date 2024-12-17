using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace StartupRegistration.Startup.StaticFiles
{
    public static class Config
    {
        public static void AddMyStaticFiles(this IServiceCollection services, IConfiguration Configuration)
        {

        }
        public static IApplicationBuilder UseMyStaticFiles(this IApplicationBuilder app, IConfiguration Configuration)
        {
            return app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (options) =>
                {
                    options.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    options.Context.Response.Headers["Pragma"] = "no-cache";
                    options.Context.Response.Headers["Expires"] = "-1";
                }
            });
        }
    }
}
