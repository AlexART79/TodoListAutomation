using OpenQA.Selenium;

namespace WebUiFramework.Elements {
  public class Form : WebElement {
    public WebElement Input => new WebElement(driver, By.CssSelector("form .input input"));
    public WebElement SubmitButton => new WebElement(driver, By.CssSelector("form .field button"));

    public Form(IWebDriver driver, By locator = null, IWebElement context = null)
        : base(driver, locator, context) { }

    public void Submit() {
      SubmitButton.Click();
    }
  }
}
