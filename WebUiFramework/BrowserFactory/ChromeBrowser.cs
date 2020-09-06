using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;

namespace WebUiFramework.BrowserFactory
{
    public class ChromeBrowser : Browser
    {
        public ChromeBrowser()
        {
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var chromeOptions = new ChromeOptions();

            // run headless
            if (Environment.GetEnvironmentVariable("RUN_HEADLESS") == "1")
            {
                chromeOptions.AddArguments("headless");
            }

            Driver = new ChromeDriver(driverPath, chromeOptions);
        }
    }
}
