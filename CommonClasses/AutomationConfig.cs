using CommonClasses.Properties;

namespace CommonClasses
{
    public sealed class AutomationConfig
    {
        private static AutomationConfig instance = null;
        private static readonly object padlock = new object();

        public string BaseUrl { get; }
        public string ApiHost { get; }
        public string DbHost { get; }
        public string DbUser { get; }
        public string DbPassword { get; }
        public string DbName { get; }


        AutomationConfig()
        {
            // load config
            BaseUrl = Settings.Default.BASE_URL;
            ApiHost = Settings.Default.API_HOST;
            DbHost = Settings.Default.DB_HOST;
            DbUser = Settings.Default.DB_USER;
            DbPassword = Settings.Default.DB_PASSWORD;
            DbName = Settings.Default.DB_NAME;
        }

        public static AutomationConfig Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AutomationConfig();
                    }
                    return instance;
                }
            }
        }
    }
}
