using System;
using System.IO;

namespace TaimeApi.Services
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
            var dotEnv = Path.Combine(appRoot, ".env");

            Load(dotEnv);
        }
    }
}