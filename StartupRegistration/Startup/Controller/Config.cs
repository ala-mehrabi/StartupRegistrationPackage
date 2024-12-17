using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GolnoorUtility.Filters;
using StartupRegistration.Startup.ModelBinding;
using Microsoft.AspNetCore.Builder;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace StartupRegistration.Startup.Controller
{
    public static class Config
    {

        public static IServiceCollection AddMyAPIControllers(this IServiceCollection services, IConfiguration Configuration, string applicationType = "API")
        {
            return (IServiceCollection)services.AddControllers(options =>
            {
                if (Configuration.GetValue<bool>($"Filters:{nameof(EnterAndLeftActionFilter)}"))
                {
                    options.Filters.Add(typeof(EnterAndLeftActionFilter));
                }
                if (Configuration.GetValue<bool>($"Filters:{nameof(ExecutionTimeAttribute)}"))
                {
                    options.Filters.Add(new ExecutionTimeAttribute());
                }
                if (Configuration.GetValue<bool>($"Filters:{nameof(ResponseCacheAttribute)}"))
                {
                    options.Filters.Add(new ResponseCacheAttribute() { Duration = 0, Location = ResponseCacheLocation.None, NoStore = true });
                }
                options.UseCustomStringModelBinder();

            }).AddJsonOptions(o =>
            {
                //تنظیمات زیر باعث میشود در لیستهای جیسانی به کامای اضافه اخر ایراد نگیرد...
                o.JsonSerializerOptions.MaxDepth = 0;
                o.JsonSerializerOptions.IgnoreNullValues = true;
                o.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                o.JsonSerializerOptions.AllowTrailingCommas = true;
            });
        }
        public static IApplicationBuilder UseMyAPIControllers(this IApplicationBuilder app, string applicationType = "API")
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    AllowCachingResponses = false,

                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                Detils = report.Entries.Select(e => new
                                {
                                    key = e.Key,
                                    value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
                endpoints.MapControllers(); // فعال کردن کنترلرها برای API
            });
        }

        public static IServiceCollection AddMyMVCControllers(this IServiceCollection services, IConfiguration Configuration, string applicationType = "MVC")
        {
            return (IServiceCollection)services.AddControllersWithViews(options =>
            {
                if (Configuration.GetValue<bool>($"Filters:{nameof(EnterAndLeftActionFilter)}"))
                {
                    options.Filters.Add(typeof(EnterAndLeftActionFilter));
                }
                if (Configuration.GetValue<bool>($"Filters:{nameof(ExecutionTimeAttribute)}"))
                {
                    options.Filters.Add(new ExecutionTimeAttribute());
                }
                if (Configuration.GetValue<bool>($"Filters:{nameof(ResponseCacheAttribute)}"))
                {
                    options.Filters.Add(new ResponseCacheAttribute() { Duration = 0, Location = ResponseCacheLocation.None, NoStore = true });
                }
                options.UseCustomStringModelBinder();

            }).AddJsonOptions(o =>
            {
                //تنظیمات زیر باعث میشود در لیستهای جیسانی به کامای اضافه اخر ایراد نگیرد...
                o.JsonSerializerOptions.MaxDepth = 0;
                o.JsonSerializerOptions.IgnoreNullValues = true;
                o.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                o.JsonSerializerOptions.AllowTrailingCommas = true;
            });
        }
        public static IApplicationBuilder UseMyMVCControllers(this IApplicationBuilder app, string applicationType = "MVC")
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    AllowCachingResponses = false,

                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                Detils = report.Entries.Select(e => new
                                {
                                    key = e.Key,
                                    value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // تنظیمات پیش‌فرض مسیر برای MVC
            });
        }

    }
}
