using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using TaimeApi.Utils.Attributes;

namespace TaimeApi.Utils.Helpers
{
    /// <summary>
    /// Helper p/ obter o ServiceProvider para verificações de injeção de dependências.
    /// </summary>
    public static class InjectionHelper
    {
        /// <summary>
        /// Provider global
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Seta o provider no InjectionCheckHelper.
        /// </summary>
        /// <param name="ServiceProvider"></param>
        public static void SetProvider(IServiceProvider ServiceProvider)
        {
            if (ServiceProvider is null)
                throw new ArgumentNullException($"{nameof(ServiceProvider)} não pode ser nulo.");

            InjectionHelper.ServiceProvider = ServiceProvider;
        }

        /// <summary>
        /// Verifica se a tecnologia está injetada no Serviço.
        /// </summary>
        /// <returns>Booleano se está injetada ou não.</returns>
        public static bool IsTecnologyInjected(Type tecnology)
        {
            var injectedOptions = ServiceProvider.GetService(tecnology);

            return !(injectedOptions == null);
        }

        /// <summary>
        /// Obtém um objeto injetado no Container de dependências.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto desejado.</typeparam>
        /// <returns>O objeto concreto injetado no Container de dependências.</returns>
        public static T GetService<T>() where T : class
        {
            if (ServiceProvider == null)
            {
                return null;
            }

            return ServiceProvider.GetService<T>();
        }

        /// <summary>
        /// Obtém um objeto injetado no Container de dependências.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto desejado.</typeparam>
        /// <returns>O objeto concreto injetado no Container de dependências.</returns>
        public static IEnumerable<T> GetServices<T>() where T : class
        {
            if (ServiceProvider == null)
            {
                return null;
            }

            return ServiceProvider.GetServices<T>();
        }

        /// <summary>
        /// Retorna um serviço obrigatório
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRequiredService<T>() where T : class
        {
            if (ServiceProvider == null)
            {
                return null;
            }

            return ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Retorna o tipo de injeção da classe informada, verificando se a classe está decorado com atributo de tipo de injeção.
        /// </summary>
        /// <param name="type">Tipo da classe.</param>
        /// <returns>O tipo de injeção configurado (ou default) da classe.</returns>
        public static InjectionType GetInjectionType(Type type)
        {
            var injectAttr = type.GetCustomAttribute<InjectionTypeAttribute>();

            //Default singleton
            if (injectAttr == null)
                return InjectionType.Singleton;

            return injectAttr.InjectionType;
        }

        /// <summary>
        /// Inclui nas propriedades para injeção de dependencia.
        /// </summary>
        /// <param name="value"></param>
        public static void Populate(object value) => Populate(ServiceProvider, value);

        /// <summary>
        /// Inclui nas propriedades para injeção de dependencia.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        public static void Populate(IServiceProvider provider, object value)
        {
            var type = value.GetType();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var currentValue = property.GetValue(value);

                if (currentValue is null && property.CanWrite)
                {
                    var newValue = provider.GetService(property.PropertyType);

                    property.SetValue(value, newValue);
                }
            }
        }
    }
}