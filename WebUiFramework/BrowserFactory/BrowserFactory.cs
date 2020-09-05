namespace WebUiFramework.BrowserFactory
{
    public enum BrowserName
    {
        Chrome,
        Firefox
    }

    public static class BrowserFactory
    {             
        public static Browser GetBrowser(BrowserName name = BrowserName.Chrome)
        {
            switch (name)
            {
                case BrowserName.Firefox:
                    return new FirefoxBrowser();                
                default:
                    return new ChromeBrowser();
            }
        }
    }
}
