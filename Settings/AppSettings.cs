namespace TaimeApi.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {
            MySqlConnectionString = Startup.GetValueFromEnv<string>("KEY_MYSQL_CONN_STR");
        }

        public string MySqlConnectionString { get; set; }
    }
}
