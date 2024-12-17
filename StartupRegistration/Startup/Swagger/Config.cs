using GolnoorUtility.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.Swagger
{
    public static class Config
    {
        public static void AddMySwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            var Scopes = Configuration.GetSection("Security:SwaggerScopes").Get<Dictionary<string, string>>();
            Settings.SwaggerScopes = Scopes.Keys.ToArray();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration.GetValue<string>("SwaggerOption:Name"), new OpenApiInfo
                {
                    Title = Configuration.GetValue<string>("SwaggerOption:Title"),
                    Version = Configuration.GetValue<string>("SwaggerOption:Version"),
                    Description = Configuration.GetValue<string>("SwaggerOption:Description")

                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]
                        {
                        }
                    }
                });

            });
        }
        public static void UseMySwagger(this IApplicationBuilder app, IConfiguration Configuration)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                c.DefaultModelsExpandDepth(0);
                c.SwaggerEndpoint($"/swagger/{Configuration.GetValue<string>("SwaggerOption:Name")}/swagger.json", Configuration.GetValue<string>("SwaggerOption:Title"));
                c.RoutePrefix = "";
                c.InjectStylesheet("/BaseResorces/swagger/custom-ui.css");
                c.InjectJavascript("/BaseResorces/swagger/custom-js.js");
                c.OAuthClientId("Swagger");
                c.OAuthAppName("swagger");
                c.OAuthUsePkce();
                //c.OAuth2RedirectUrl($"{Configuration["Const:BaseUrl"]}/oauth2-redirect.html");
            });
        }
    }
}
