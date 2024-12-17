using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupRegistration.Startup.Corses
{
    public static class Config
    {
        #region DisableCors
        public static IServiceCollection AddMyDisableCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("DisableCors", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //  .AllowCredentials()
                    .Build();
                });
            });
        }

        public static IApplicationBuilder UseMyDisableCors(this IApplicationBuilder app, string policyName)
        {
            return app.UseCors(policyName);
        }

        #endregion DisableCors

        #region DisableAnyOriginCors
        public static IServiceCollection AddMyDisableAnyOriginCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("DisableCors", builder =>
                {
                    builder
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //  .AllowCredentials()
                    .Build();
                });
            });
        }

        public static IApplicationBuilder UseMyDisableAnyOriginCors(this IApplicationBuilder app, string policyName)
        {
            return app.UseCors(policyName);
        }
        #endregion DisableAnyOriginCors

        #region DisableAnyMethodCors
        public static IServiceCollection AddMyDisableAnyMethodCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("DisableCors", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    //.AllowAnyMethod()
                    .AllowAnyHeader()
                    //  .AllowCredentials()
                    .Build();
                });
            });
        }

        public static IApplicationBuilder UseMyDisableAnyMethodCors(this IApplicationBuilder app, string policyName)
        {
            return app.UseCors(policyName);
        }
        #endregion DisableAnyOriginCors

        #region DisableAnyHeaderCors
        public static IServiceCollection AddMyDisableAnyHeaderCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("DisableCors", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    //.AllowAnyHeader()
                    //  .AllowCredentials()
                    .Build();
                });
            });
        }

        public static IApplicationBuilder UseMyDisableAnyHeaderCors(this IApplicationBuilder app, string policyName)
        {
            return app.UseCors(policyName);
        }
        #endregion DisableAnyOriginCors

    }
}
