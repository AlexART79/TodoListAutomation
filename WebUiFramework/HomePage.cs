using OpenQA.Selenium;
using WebUiFramework.Elements;

namespace WebUiFramework {
  public class HomePage {
    private IWebDriver _driver;

    public HomePage(Browser browser) {
      this._driver = browser.Driver;
    }

    public WebElement Header => new WebElement(_driver, By.CssSelector(".list-header")).WaitFor();

    public TodoList List => new TodoList(_driver, By.CssSelector("div[role=list]")).WaitFor() as TodoList;

    public Form Form => new Form(_driver, By.CssSelector("form")).WaitFor() as Form;


    public void Open(string url) {
      _driver.Navigate().GoToUrl(url);
    }
  }
}
