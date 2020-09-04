using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace WebUiFramework
{
    public class Browser
    {
        readonly IWebDriver driver;

        public Browser()
        {
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(driverPath);
        }

        public IWebDriver Driver => driver;

        public void Quit()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
