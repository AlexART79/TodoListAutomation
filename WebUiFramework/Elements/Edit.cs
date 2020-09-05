using OpenQA.Selenium;

namespace WebUiFramework.Elements
{
    public class Edit : WebElement
    {
        public Edit(IWebDriver driver, By locator = null, IWebElement context = null)
            : base(driver, locator, context)
        { }

        public string Value
        {
            get
            {
                return Element.GetAttribute("value");
            }
            set
            {
                SendKeys(value);
            }
        }
    }
}
