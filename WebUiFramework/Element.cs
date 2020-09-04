using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUiFramework
{
    public class Element
    {
        readonly IWebDriver driver;
        readonly By locator;
        readonly Element context;

        IWebElement _element;

        public Element(IWebDriver driver, By locator=null, Element context=null)
        {
            this.driver = driver;
            this.locator = locator;
            this.context = context;

            _element = null;
        }

        public IWebElement WebElement
        {
            get
            {
                if (_element != null)
                    return _element;

                return context != null ?                
                    context.WebElement.FindElement(locator) :
                    driver.FindElement(locator);
            }
        }

        public Element Wrap(IWebElement element)
        {
            _element = element;
            return this;
        }

        public void Click()
        {

        }
    }

    public class Edit : Element
    {
        public Edit (IWebDriver driver, By locator = null, Element context = null)
            :base(driver, locator, context)
        { }

        public string Value => WebElement.GetAttribute("value");

    }
}
