using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TaimeApi.Data.MySql;
using TaimeApi.Enums;
using TaimeApi.Services;
using TaimeApi.Settings;
using TaimeApi.Utils;
using TaimeApi.Utils.Extensions;

namespace TaimeApi
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration, typeof(TaimeApiErrors)) { }

        public override void BeforeConfigureServices(IServiceCollection services)
        {
            services.AddMySql(GetValueFromEnv<string>("KEY_MYSQL_CONN_STR"));

            var appSettings = new AppSettings();
            services.AddSingleton(appSettings);
        }

        //public static T GetValueFromEnv<T>(string propertyName)
        //{
        //    var convertedValue = Environment.GetEnvironmentVariable(propertyName);
        //    return (T)Convert.ChangeType(convertedValue, typeof(T));
        //}
    }
}
