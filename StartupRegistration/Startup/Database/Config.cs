using GolnoorUtility.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.Database
{
    public static class Config
    {
        public static void AddMyDataBaseContext(this IServiceCollection services, IConfiguration configuration, string SecretKey, params Type[] dbContextTypes)
        {
            var dbContextsSection = configuration.GetSection("DbContexts").GetChildren();
            var isEncrypt = configuration.GetValue<bool>("IsEncrypt");
            foreach (var dbContextSection in dbContextsSection)
            {
                var dbContextName = dbContextSection.Key;
                var connectionString = dbContextSection.GetValue<string>("ConnectionString");
                var isEncrypted = dbContextSection.GetValue<bool>("IsEncrypted");

                // بررسی نیاز به رمزگشایی
                if (isEncrypt && isEncrypted)
                {
                    connectionString = Extention.DecryptString(connectionString, SecretKey);
                }

                // پیدا کردن Type مربوط به DbContext از طریق reflection
                var dbContextType = Type.GetType($"{dbContextName},Core.DataBaseContex");
                if (dbContextType == null)
                {
                    throw new Exception($"DbContext type '{dbContextName}' not found.");
                }

                // پیدا کردن متد AddDbContextPool با استفاده از reflection
                var method = typeof(EntityFrameworkServiceCollectionExtensions)
    .GetMethods()
    .FirstOrDefault(m => m.Name == "AddDbContextPool" && m.IsGenericMethod &&
                         m.GetParameters().Length == 3 &&
                         m.GetParameters()[0].ParameterType == typeof(IServiceCollection) &&
                         m.GetParameters()[1].ParameterType == typeof(Action<DbContextOptionsBuilder>) &&
                         m.GetParameters()[2].ParameterType == typeof(int))
    ?.MakeGenericMethod(dbContextType);

                if (method == null)
                {
                    throw new Exception($"Failed to find method AddDbContextPool for '{dbContextType.Name}'.");
                }

                // فراخوانی متد AddDbContextPool به صورت داینامیک
                method.Invoke(null, new object[] { services, (Action<DbContextOptionsBuilder>)(options => options.UseSqlServer(connectionString, p => p.CommandTimeout(3 * 60))), 128 });

            }
        }
    }
}
