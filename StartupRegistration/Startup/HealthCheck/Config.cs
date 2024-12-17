using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.HealthCheck
{
    public static class Config
    {
        public static IHealthChecksBuilder AddMyHealthCheck(this IServiceCollection services)
        {
            return services.AddHealthChecks();
        }
        public static HealthChecksUIBuilder AddMyHealthCheckUI(this IServiceCollection services)
        {
            return services.AddHealthChecksUI(p =>
            {
                p.AddHealthCheckEndpoint("SelfHealthCheck", "/hc");
            }).AddInMemoryStorage();
        }
        public static void UseMyHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                AllowCachingResponses = false,

                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        }
        public static void UseMyHealthCheckUI(this IApplicationBuilder app)
        {
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/hc-ui";
                options.ApiPath = "/hc-ui-api";
                options.AddCustomStylesheet("wwwroot/BaseResorces/HelthyCheck/custom-ui.css");
            });
        }
        public static IEndpointConventionBuilder MapMyHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/healthcheck");
        }
        public static IEndpointConventionBuilder MapMyHealthChecksUI(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecksUI(config =>
            {
                config.UIPath = "/hc-ui";
                config.ApiPath = "/hc";
                config.AddCustomStylesheet("wwwroot/BaseResorces/HelthyCheck/custom-ui.css");
            });
        }
    }
}
