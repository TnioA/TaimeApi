using System.Runtime.CompilerServices;

namespace Taime.Application.Helpers
{
    public static class EnvLoaderHelper
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        public static void Load()
        {
            var appRoot = Directory.GetCurrentDirectory();
            var dotEnv = Path.Combine(appRoot, "../.env");

            Load(dotEnv);
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
    }
}