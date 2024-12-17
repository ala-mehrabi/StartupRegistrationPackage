using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;

namespace StartupRegistration.Startup.Compression
{
    public static class Config
    {
        public static IServiceCollection AddMyResponseCompression(this IServiceCollection services)
        {
            return services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

        }

        public static IServiceCollection AddMyGzipCompression(this IServiceCollection services, System.IO.Compression.CompressionLevel GzipcompressionLevel)
        {
            return services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = GzipcompressionLevel;
            });
        }

        public static IServiceCollection AddMyBrotliCompression(this IServiceCollection services,System.IO.Compression.CompressionLevel BrotlicompressionLevel)
        {
            return services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = BrotlicompressionLevel;
            });
        }

        public static IApplicationBuilder UseMyCompression(this IApplicationBuilder app)
        {
           return app.UseResponseCompression();
        }
    }
}
