using OpenQA.Selenium;
using WebUiFramework.Elements;

namespace WebUiFramework
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(Browser browser)
        {
            this.driver = browser.Driver;
        }

        public WebElement Header => new WebElement(driver, By.CssSelector(".list-header")).WaitFor();

        public TodoList List => new TodoList(driver, By.CssSelector("div[role=list]")).WaitFor() as TodoList;

        public Form Form => new Form(driver, By.CssSelector("form")).WaitFor() as Form;
                

        public void Open(string url)
        {            
            driver.Navigate().GoToUrl(url);
        }
    }
}
