using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;

namespace TaimeApi.Utils.Helpers
{
    /// <summary>
    /// Operações para auxiliar na manipulação de objetos por reflexão.
    /// </summary>
    public static class ReflectionHelper
    {
        private static readonly HashSet<Type> _types;

        internal static readonly HashSet<Assembly> Assemblies;

        static ReflectionHelper()
        {
            Assemblies = new HashSet<Assembly>(GetLocalAssemblies());

            _types = new HashSet<Type>(Assemblies.SelectMany(t => t.GetTypes()).ToArray());
        }

        /// <summary>
        /// Lista todas as classes do Assembly que implementam a interface informada.
        /// </summary>
        /// <param name="interfaceType">Interface implementada pelos tipos.</param>
        /// <returns>Lista de tipos implementam a interface.</returns>
        public static IEnumerable<Type> ListClassesImplements(Type interfaceType)
        {
            return _types
                .Where(t => t.GetInterfaces().Contains(interfaceType));
        }

        /// <summary>
        /// Lista todas as classes do Assembly que implementam a interface informada.
        /// </summary>
        /// <param name="interfaceType">Interface implementada pelos tipos.</param>
        /// <returns>Lista de tipos implementam a interface.</returns>
        public static IEnumerable<Type> ListClassesImplementsFromGeneric(Type interfaceType)
        {
            return _types
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
        }

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo genérico.
        /// </summary>
        /// <param name="type">Tipo do genérico que será buscado as classes que o herdam.</param>
        /// <returns>Lista de tipos que herdam o genérico.</returns>
        public static IEnumerable<Type> ListClassesInheritFromGeneric(Type type)
        {
            return _types
                .Where(t =>
                    t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == type);
        }

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo
        /// </summary>
        /// <param name="type">Tipo que será buscado as classes que o herdam.</param>
        /// <returns>Lista de tipos que herdam</returns>
        public static IEnumerable<Type> ListClassesInherit(Type type)
        {
            return _types
                .Where(t => IsInherit(t, type));
        }

        private static bool IsInherit(Type type, Type baseTypeExpected)
        {
            if (type.BaseType == null)
                return false;

            if (type.BaseType == baseTypeExpected)
                return true;

            return IsInherit(type.BaseType, baseTypeExpected);
        }

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo.
        /// </summary>
        /// <param name="type">Tipo do genérico que será buscado as classes que o herdam.</param>
        /// <returns>Lista de tipos que herdam o genérico.</returns>
        public static IEnumerable<Type> ListClassesInheritFromType(Type type)
        {
            return _types.Where(t => t.BaseType != null && t.BaseType == type);
        }

        public static IEnumerable<Assembly> GetLocalAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = new List<Assembly>();

            var assemblyInfo = Assembly.GetExecutingAssembly().FullName.Split(',');
            var namespaces = assemblyInfo[0].Split('.');
            result.AddRange(assemblies.Where(x => x.FullName.StartsWith(namespaces[0])));

            assemblyInfo = Assembly.GetEntryAssembly().FullName.Split(',');
            namespaces = assemblyInfo[0].Split('.');
            result.AddRange(assemblies.Where(x => x.FullName.StartsWith(namespaces[0])));

            var dependencyAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();

            var internalDependencies = dependencyAssemblies.Where(a => a.FullName.StartsWith(namespaces[0]));

            if (internalDependencies.Any())
            {
                foreach (var internalDependency in internalDependencies)
                {
                    var appAssemblies = Assembly.Load(internalDependency);
                    if (appAssemblies != null)
                    {
                        result.Add(appAssemblies);
                    }
                }
            }

            //var dharma = result.SelectMany(x => x.GetReferencedAssemblies()).Where(i => i.FullName.StartsWith("Dharma")).Select(i => Assembly.Load(i)).ToList();
            //result.AddRange(dharma);

            //if (ApplicationHelper.MicroServiceName() == ApplicationHelper.TESTAPPLICATION)
            //{
            //    var testAppName = ApplicationHelper.TestApplicationName();

            //    if (!IsOSPlatform(OSPlatform.Windows))
            //    {
            //        testAppName = testAppName.Substring(testAppName.LastIndexOf("/", StringComparison.Ordinal)).TrimStart('/');
            //    }

            //    result.AddRange(assemblies.Where(x => x.FullName.StartsWith(testAppName)));
            //}

            return result.Distinct();
        }

        /// <summary>Determina se o tipo é instanciável.</summary>
        /// <param name="type">O tipo para testar.</param>
        /// <returns><c>true</c> se o tipo é instanciável; senão, <c>false</c>.</returns>
        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract &&
                   !type.IsGenericTypeDefinition &&
                   !type.IsGenericParameter;
        }
    }
}