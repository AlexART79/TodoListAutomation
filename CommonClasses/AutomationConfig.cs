using CommonClasses.Properties;

namespace CommonClasses {
  /// <summary>
  /// Singletone Config
  /// </summary>
  public sealed class AutomationConfig {

    private static AutomationConfig _instance = null;
    private static readonly object _padlock = new object();

    public string BaseUrl { get; }
    public string ApiHost { get; }
    public string DbHost { get; }
    public string DbUser { get; }
    public string DbPassword { get; }
    public string DbName { get; }

    public static AutomationConfig Instance {
      get {
        lock (_padlock) {
          if (_instance == null) {
            _instance = new AutomationConfig();
          }
          return _instance;
        }
      }
    }

    AutomationConfig() {
      // load config
      BaseUrl = Settings.Default.BASE_URL;
      ApiHost = Settings.Default.API_HOST;
      DbHost = Settings.Default.DB_HOST;
      DbUser = Settings.Default.DB_USER;
      DbPassword = Settings.Default.DB_PASSWORD;
      DbName = Settings.Default.DB_NAME;
    }    
  }
}
