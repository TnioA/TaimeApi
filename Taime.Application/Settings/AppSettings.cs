using static Taime.Application.Helpers.EnvLoaderHelper;

namespace Taime.Application.Settings
{
    public class AppSettings
    {
        public string JWTAuthorizationKey { get; set; }
        
        public int JWTAccessTokenExpirationTime { get; set; }

        public int JWTRefreshTokenExpirationTime { get; set; }

        public string MySqlConnectionString { get; set; }

        public AppSettings()
        {
            JWTAuthorizationKey = GetValueFromEnv<string>("JWT_AUTH_KEY");
            JWTAccessTokenExpirationTime = GetValueFromEnv<int>("JWT_ACCESS_TOKEN_EXPIRATION_TIME");
            JWTRefreshTokenExpirationTime = GetValueFromEnv<int>("JWT_REFRESH_TOKEN_EXPIRATION_TIME");
            MySqlConnectionString = GetValueFromEnv<string>("KEY_MYSQL_CONN_STR");
        }
    }
}
