using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Taime.Application.Data.MySql;
using Taime.Application.Utils.Data.Api;
using Taime.Application.Utils.Data.MySql;
using Taime.Application.Utils.Helpers;
using Taime.Application.Utils.Services;

namespace Taime.Application.Utils.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            IEnumerable<Type> enumerable = ReflectionHelper.ListClassesInherit(typeof(BaseService));
            foreach (Type type in enumerable)
            {
                services.AddScoped(type);
            }

            return services;
        }

        public static IServiceCollection AddMySql(this IServiceCollection services, string mySqlContextStr)
        {
            services.AddDbContext<MySqlContext>(options => options.UseMySql(mySqlContextStr, ServerVersion.AutoDetect(mySqlContextStr)));

            var repositories = ReflectionHelper.ListClassesInherit(typeof(RepositoryBase));
            foreach (Type repository in repositories)
            {
                services.AddScoped(repository);
            }

            return services;
        }

        public static IServiceCollection AddApiCallRepositories(this IServiceCollection services)
        {
            if (!services.ExistServiceType<IHttpContextAccessor>())
                services.AddHttpContextAccessor();

            services.AddHttpClient();

            var repositories = ReflectionHelper.ListClassesInherit(typeof(HTTPApiCallRepository));
            foreach (Type repository in repositories)
            {
                services.AddScoped(repository);
            }

            return services;
        }

        public static bool ExistServiceType<T>(this IServiceCollection services)
        {
            return services.FirstOrDefault(s => s.ServiceType == typeof(T)) != null;
        }
    }
}