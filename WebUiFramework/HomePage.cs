using OpenQA.Selenium;

namespace WebUiFramework
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(Browser browser)
        {
            this.driver = browser.Driver;
        }

        public IWebElement Header => driver.FindElement(By.CssSelector(".list-header"));

        public IWebElement List => driver.FindElement(By.CssSelector("div[role=list]"));

        public IWebElement Form => driver.FindElement(By.CssSelector("form"));

        public IWebElement Input => driver.FindElement(By.CssSelector("form .input input"));

        public IWebElement Submit => driver.FindElement(By.CssSelector("form .field button"));

        public void Open(string url)
        {            
            driver.Navigate().GoToUrl(url);
        }
    }
}
