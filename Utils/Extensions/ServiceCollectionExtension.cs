using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaimeApi.Data.MySql;
using TaimeApi.Utils.Attributes;
using TaimeApi.Utils.Data.MySql;
using TaimeApi.Utils.Helpers;
using TaimeApi.Utils.Services;

namespace TaimeApi.Utils.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            IEnumerable<Type> enumerable = ReflectionHelper.ListClassesInherit(typeof(BaseService));
            foreach (var item in enumerable)
            {
                services.AddDynamicScope(item);
            }

            Type repositoryType = typeof(IRepositoryBase<>);
            IEnumerable<Type> enumerable2 = from t in ReflectionHelper.ListClassesImplementsFromGeneric(repositoryType) where t.IsConcrete() select t;
            foreach (var item in enumerable2)
            {
                Type serviceType = item.GetInterfaces().FirstOrDefault((i) => i.IsGenericType && i.GetGenericTypeDefinition() == repositoryType);
                InjectionType injectionType = GetInjectionType(item);
                if (injectionType == InjectionType.Scoped)
                    services.AddScoped(serviceType, item);
                else
                    services.AddSingleton(serviceType, item);
            }

            return services;
        }

        public static bool ExistServiceType<T>(this IServiceCollection services)
        {
            return services.FirstOrDefault(s => s.ServiceType == typeof(T)) != null;
        }

        public static IServiceCollection AddDynamicScope(this IServiceCollection services, Type type)
        {
            if (type.IsAbstract)
                return services;

            var repositoryInterface = type.GetInterface($"I{type.Name}", true);
            if (repositoryInterface == null)
            {
                switch (GetInjectionType(type))
                {
                    case InjectionType.Scoped:
                        services.AddScoped(type);
                        break;

                    default:
                        services.AddSingleton(type);
                        break;
                }
            }
            else
            {
                switch (GetInjectionType(type))
                {
                    case InjectionType.Scoped:
                        services.AddScoped(repositoryInterface, type);
                        services.AddScoped(type);
                        break;

                    default:
                        services.AddSingleton(repositoryInterface, type);
                        services.AddSingleton(type);
                        break;
                }

                services.AddDecorators(repositoryInterface, type);
            }

            return services;
        }

        public static IServiceCollection AddDecorators(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            var decorators = ReflectionHelper.ListClassesImplements(interfaceType).Where(p => p.Name.StartsWith($"{implementationType.Name}Decorator"));
            foreach (var decorator in decorators)
            {
                services.AddDecorators(interfaceType, decorator);
            }

            return services;
        }

        public static IServiceCollection AddMySql(this IServiceCollection services, string mySqlContext)
        {
            if (string.IsNullOrWhiteSpace(mySqlContext))
                throw new ArgumentNullException("'MySqlContext' deve ser informado.");

            services.AddDbContext<MySqlProvider>(options => options.UseMySql(mySqlContext, ServerVersion.AutoDetect(mySqlContext)));
            var repositories = ReflectionHelper.ListClassesInherit(typeof(RepositoryBase));
            foreach (Type repository in repositories)
            {
                services.AddDynamicScope(repository);
            }

            return services;
        }

        public static InjectionType GetInjectionType(Type type)
        {
            var injectAttr = type.GetCustomAttribute<InjectionTypeAttribute>();
            if (injectAttr == null)
                return InjectionType.Singleton;

            return injectAttr.InjectionType;
        }
    }
}