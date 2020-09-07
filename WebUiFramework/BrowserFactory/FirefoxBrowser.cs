using OpenQA.Selenium.Firefox;
using System.IO;
using System.Reflection;

namespace WebUiFramework.BrowserFactory {
  public class FirefoxBrowser : Browser {
    public FirefoxBrowser() {
      var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Driver = new FirefoxDriver(driverPath);
    }
  }
}
