using CommonClasses;
using OpenQA.Selenium;
using System;

namespace WebUiFramework {
  public class WebElement {
    protected readonly IWebDriver driver;
    protected readonly By locator;
    protected readonly IWebElement context;

    protected IWebElement _element;

    public WebElement(IWebDriver driver, By locator = null, IWebElement context = null) {
      this.driver = driver;
      this.locator = locator;
      this.context = context;

      _element = null;
    }

    public IWebElement Element {
      get {
        if (_element != null) {
          return _element;
        }

        try {
          var element = context != null ?
              context.FindElement(locator) :
              driver.FindElement(locator);

          // in case of stale element it will except...
          var txt = element.Text;
          
          return element;
        }
        catch (StaleElementReferenceException) {           
          return null;
        }
        catch (NoSuchElementException) {           
          return null;
        }
      }
    }

    public string Text => Element?.Text;
    public bool Complete {
      get {
        return (bool)(Element?.GetAttribute("class").Contains("todo-list-item-checked"));
      }
    }

    public bool Exists => Element != null;
    public bool Displayed => (bool)(Element?.Displayed);

    public WebElement Wrap(IWebElement element) {
      _element = element;
      return this; // Chain invocation pattern
    }

    public WebElement Click() {
      Element?.Click();
      return this; // Chain invocation pattern
    }

    public WebElement SendKeys(string text) {
      Element?.SendKeys(text);
      return this; // Chain invocation pattern
    }

    public WebElement WaitFor(int seconds = 30) {
      Helpers.Wait(() => this.Exists, TimeSpan.FromSeconds(seconds));
      return this; // Chain invocation pattern
    }
  }
}
