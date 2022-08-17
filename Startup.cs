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
using TaimeApi.Data.MySql;
using TaimeApi.Enums;
using TaimeApi.Helpers;
using TaimeApi.Settings;
using TaimeApi.Utils.Extensions;
using TaimeApi.Utils.Helpers;

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
            // Set minimum threads
            int minimunWorker, minimunIOC;
            ThreadPool.GetMinThreads(out minimunWorker, out minimunIOC);
            ThreadPool.SetMinThreads(250, minimunWorker);

            AddDataBaseRepositories(services);
            AddApiCallRepositories(services);
            services.AddControllers();
            var settings = new AppSettings();
            services.AddSingleton(settings);

            if (!services.ExistServiceType<IHttpContextAccessor>())
                services.AddHttpContextAccessor();

            // INJETANDO SERVICES OBS: VERIFICAR SE É A FORMA CORRETA
            //var allProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
            //    .GetTypes().Where(t => t.Name != null && t.Name.Contains("Service"));

            //foreach (var intfc in allProviderTypes.Where(t => t.IsClass))
            //{
            //    var impl = allProviderTypes.FirstOrDefault(c => c.IsClass && intfc.Name == c.Name);
            //    if (impl != null) services.AddScoped(intfc, impl);
            //}

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

            // adding versioning
            services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {

                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // add swagger
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
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement { openApiSecurityScheme, new string[0] });
            });
            services.ConfigureOptions<ConfigureSwaggerExtension>();


            // auto inject services
            services.AddBaseServices();
        }

        #endregion

        #region [Configure App]

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InjectionHelper.SetProvider(app.ApplicationServices);
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.ConfigObject.DisplayRequestDuration = true;
                options.DocumentTitle = "Swagger - " + GetSwaggerApplicationName();
                IApiVersionDescriptionProvider provider = InjectionHelper.GetService<IApiVersionDescriptionProvider>();
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
            //app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
        }

        #endregion

        #region [Auxiliar Methods]

        protected virtual void AddDataBaseRepositories(IServiceCollection services)
        {
            services.AddMySql(GetValueFromEnv<string>("KEY_MYSQL_CONN_STR"));

            var appSettings = new AppSettings();
            services.AddSingleton(appSettings);
        }

        protected virtual void AddApiCallRepositories(IServiceCollection services)
        {
            services.AddHttpClient();

            //var all = ReflectionHelper.ListClassesInherit(typeof(HTTPApiCallRepository));

            //foreach (var item in all)
            //{
            //    services.AddDynamicScope(item);
            //}
        }

        public static string GetValueFromEnv(string keyName, bool throwException = true, [CallerMemberName] string section = "unknown")
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentNullException(nameof(keyName), "Informe um nome de chave.");
            }

            return GetEnvironmentVariable(keyName, "keyname", throwException, section);
        }

        public static T GetValueFromEnv<T>(string keyName, bool throwException = true, [CallerMemberName] string section = "unknown")
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentNullException(nameof(keyName), "Informe um nome de chave.");
            }

            var value = GetEnvironmentVariable(keyName, "keyname", throwException, section);

            if (!string.IsNullOrWhiteSpace(value))
                return (T)Convert.ChangeType(value, typeof(T));

            return default;
        }

        public static string GetEnvironmentVariable(string envName, string paramName, bool throwException = true, [CallerMemberName] string section = "unknown")
        {
            if (string.IsNullOrWhiteSpace(envName))
            {
                throw new ArgumentOutOfRangeException(paramName,
                    $"{section}: o parâmetro '{paramName}' deve ser informado (não pode ser vazio ou nulo).");
            }

            var value = Environment.GetEnvironmentVariable(envName);

            if (string.IsNullOrWhiteSpace(value) && throwException)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    $"{section}: não existe ou está vazio o valor para a variável de ambiente '{envName}'.");
            }

            return value;
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
