using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.Localization
{
    public static class Config
    {
        public static void AddMyLocalization(this IServiceCollection services)
        {
            services.AddLocalization();
        }
        public static IApplicationBuilder UseMyLocalization(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("fa-IR") };

            return app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

        }
    }
}
