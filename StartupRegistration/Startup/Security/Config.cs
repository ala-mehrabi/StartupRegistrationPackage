using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StartupRegistration.Startup.Security
{
    public static class Config
    {
        public static void AddMySecurity(this IServiceCollection services, IConfiguration Configuration)
        {


        }

        public static void UseMySecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
