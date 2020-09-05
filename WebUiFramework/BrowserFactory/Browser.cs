using OpenQA.Selenium;

namespace WebUiFramework
{
    public abstract class Browser
    {
        public IWebDriver Driver { get; protected set; }

        public void Quit()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
        }
    } 
}
