using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using TaimeApi.Application.Helpers;
using TaimeApi.Application.Settings;
using TaimeApi.Application.Utils.Data.Api;
using TaimeApi.Application.Utils.Extensions;
using TaimeApi.Application.Utils.Helpers;

namespace TaimeApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            EnvLoaderHelper.Load();
        }

        #region [Configure Services]

        public void ConfigureServices(IServiceCollection services)
        {
            // Set Minimum Threads
            int minimunWorker, minimunIOC;
            ThreadPool.GetMinThreads(out minimunWorker, out minimunIOC);
            ThreadPool.SetMinThreads(250, minimunWorker);

            // Set Data Services
            AddDataBaseRepositories(services);
            AddApiCallRepositories(services);
            if (!services.ExistServiceType<IHttpContextAccessor>())
                services.AddHttpContextAccessor();

            // Set Controllers
            services.AddControllers();

            // Set Settings
            var settings = new AppSettings();
            services.AddSingleton(settings);

            // Set Authentication
            var key = Encoding.ASCII.GetBytes(settings.JWTAuthorizationToken);
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions => {
                configureOptions.RequireHttpsMetadata = true;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Set Api Versioning
            services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Set Documentation
            services.AddSwaggerGen(options =>
            {
                OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "oauth2"
                };

                options.AddSecurityDefinition("Bearer", openApiSecurityScheme);
            });
            services.ConfigureOptions<ConfigureSwaggerExtension>();

            // Set Auto Inject Services
            services.AddBaseServices();
        }

        #endregion

        #region [Configure App]

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.ConfigObject.DisplayRequestDuration = true;
                options.DocumentTitle = "Swagger - " + GetSwaggerApplicationName();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        #endregion

        #region [Auxiliar Methods]

        protected virtual void AddDataBaseRepositories(IServiceCollection services)
        {
            services.AddMySql(EnvLoaderHelper.GetValueFromEnv<string>("KEY_MYSQL_CONN_STR"));

            var appSettings = new AppSettings();
            services.AddSingleton(appSettings);
        }

        protected virtual void AddApiCallRepositories(IServiceCollection services)
        {
            services.AddHttpClient();

            var all = ReflectionHelper.ListClassesInherit(typeof(HTTPApiCallRepository));
            foreach (var item in all)
            {
                services.AddDynamicScope(item);
            }
        }

        public virtual string GetSwaggerApplicationName()
        {
            var applicationName = PlatformServices.Default.Application.ApplicationName;

            applicationName = applicationName.Split(".")[0];

            return applicationName;
        }

        #endregion
    }
}
