using CommonClasses;
using OpenQA.Selenium;
using System;

namespace WebUiFramework
{
    public class WebElement
    {
        protected readonly IWebDriver driver;
        protected readonly By locator;
        protected readonly IWebElement context;

        protected IWebElement _element;

        public WebElement(IWebDriver driver, By locator=null, IWebElement context=null)
        {
            this.driver = driver;
            this.locator = locator;
            this.context = context;

            _element = null;
        }

        public IWebElement Element
        {
            get
            {
                if (_element != null)
                    return _element;
                
                try
                {
                    return context != null ?
                        context.FindElement(locator) :
                        driver.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public string Text => Element.Text;
        public bool Displayed => Element.Displayed;

        public WebElement Wrap(IWebElement element)
        {
            _element = element;
            return this;
        }

        public WebElement Click()
        {
            Element.Click();
            return this;
        }
        
        public WebElement SendKeys(string text)
        {
            Element.SendKeys(text);
            return this;
        }

        public WebElement WaitFor(int seconds = 30)
        {
            Helpers.Wait(() => Element != null, TimeSpan.FromSeconds(seconds));
            return this;
        }
    }
}
