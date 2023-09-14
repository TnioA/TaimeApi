using static Taime.Application.Helpers.EnvLoaderHelper;

namespace Taime.Application.Settings
{
    public class AppSettings
    {
        public string JWTAuthorizationToken { get; set; }
        
        public int JWTTokenExpirationTime { get; set; }
        
        public string MySqlConnectionString { get; set; }

        public AppSettings()
        {
            JWTAuthorizationToken = GetValueFromEnv<string>("JWT_AUTH_TOKEN");
            JWTTokenExpirationTime = GetValueFromEnv<int>("JWT_TOKEN_EXPIRATION_TIME");
            MySqlConnectionString = GetValueFromEnv<string>("KEY_MYSQL_CONN_STR");
        }
    }
}
