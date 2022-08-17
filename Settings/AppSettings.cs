using static TaimeApi.Startup;

namespace TaimeApi.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {
            JWTAuthorizationToken = GetValueFromEnv<string>("JWT_AUTH_TOKEN");
            JWTTokenExpirationTime = GetValueFromEnv<int>("JWT_TOKEN_EXPIRATION_TIME");
            MySqlConnectionString = GetValueFromEnv<string>("KEY_MYSQL_CONN_STR"); 
        }

        public string JWTAuthorizationToken { get; set; }
        public int JWTTokenExpirationTime { get; set; }
        public string MySqlConnectionString { get; set; }
    }
}
