using System.Reflection;

namespace Taime.Application.Utils.Helpers
{
    /// <summary>
    /// Operações para auxiliar na manipulação de objetos por reflexão.
    /// </summary>
    public static class ReflectionHelper
    {
        internal static readonly HashSet<Assembly> Assemblies;
        private static readonly HashSet<Type> _types;

        static ReflectionHelper()
        {
            Assemblies = new HashSet<Assembly>(GetLocalAssemblies());
            _types = new HashSet<Type>(Assemblies.SelectMany(t => t.GetTypes()).ToArray());
        }

        /// <summary>
        /// Lista todas as classes do Assembly que implementam a interface informada.
        /// </summary>
        public static IEnumerable<Type> ListClassesImplements(Type interfaceType) =>
            _types.Where(t => t.GetInterfaces().Contains(interfaceType));

        /// <summary>
        /// Lista todas as classes genéricas do Assembly que implementam a interface informada.
        /// </summary>
        public static IEnumerable<Type> ListClassesImplementsFromGeneric(Type interfaceType) =>
            _types.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo
        /// </summary>
        public static IEnumerable<Type> ListClassesInherit(Type type) =>
            _types.Where(t => IsInherit(t, type));

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo genérico.
        /// </summary>
        public static IEnumerable<Type> ListClassesInheritFromGeneric(Type type) =>
            _types.Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == type);

        private static bool IsInherit(Type type, Type baseTypeExpected)
        {
            if (type.BaseType == null)
                return false;

            if (type.BaseType == baseTypeExpected)
                return true;

            return IsInherit(type.BaseType, baseTypeExpected);
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

            return result.Distinct();
        }

        /// <summary>
        /// Verifica se o tipo é instanciavel
        /// </summary>
        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract &&
                   !type.IsGenericTypeDefinition &&
                   !type.IsGenericParameter;
        }
    }
}