using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace WebUiFramework.BrowserFactory
{
    public class ChromeBrowser : Browser
    {
        public ChromeBrowser()
        {
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Driver = new ChromeDriver(driverPath);
        }
    }
}
