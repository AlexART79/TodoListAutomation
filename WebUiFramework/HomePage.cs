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

        public IWebElement Header 
        {
            get {
                var e = driver.FindElement(By.CssSelector(".list-header"));
                return e;
            }
        }
        
        public IWebElement List
        {
            get
            {
                var e = driver.FindElement(By.CssSelector("div[role=list]"));
                return e;
            }
        }

        public IWebElement Form
        {
            get
            {
                var e = driver.FindElement(By.CssSelector("form"));
                return e;
            }
        }

        public IWebElement Input
        {
            get
            {
                var e = driver.FindElement(By.CssSelector("form .input input"));
                return e;
            }
        }

        public IWebElement Submit
        {
            get
            {
                var e = driver.FindElement(By.CssSelector("form .field button"));
                return e;
            }
        }


        public void Open(string url)
        {            
            driver.Navigate().GoToUrl(url);
        }

    }
}
