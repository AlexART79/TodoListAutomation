using OpenQA.Selenium;

namespace WebUiFramework.Elements
{
    public class Button : WebElement
    {
        public Button(IWebDriver driver, By locator = null, IWebElement context = null)
            : base(driver, locator, context)
        { }
    }
}
