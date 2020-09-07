using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace WebUiFramework {
  public abstract class Browser {
    public IWebDriver Driver { get; protected set; }

    public byte[] TakeScreenshot() {
      return Driver.TakeScreenshot().AsByteArray;
    }

    public string TakeScreenshotBase64() {
      return Driver.TakeScreenshot().AsBase64EncodedString;
    }

    public void Quit() {

      if (Driver != null) {
        Driver.Quit();
      }
    }
  }
}
