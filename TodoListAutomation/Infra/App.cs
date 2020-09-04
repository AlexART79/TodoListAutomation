using DBFramework;
using RestFramework;
using System;
using WebUiFramework;
using TodoListAutomation.Properties;

namespace TodoListAutomation
{
    public class App : IDisposable
    {
        private bool disposedValue;

        public readonly string ApiUrl = Settings.Default.API_HOST;
        public readonly string AppUrl = Settings.Default.BASE_URL;

        public Browser Browser { get; set; }
        public HomePage HomePage { get; set; }
        public TodoList TodoListDb { get; set; }
        public ApiClient TodoListApi { get; set; }

        public App()
        {
            Browser = new Browser();
            HomePage = new HomePage(Browser);

            TodoListDb = new TodoList();
            TodoListApi = new ApiClient(ApiUrl);
        }

        public void Close()
        {
            if (Browser != null)
            {
                Browser.Quit();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Close();
                }

                Browser = null;
                HomePage = null;
                TodoListDb = null;
                TodoListApi = null;

                disposedValue = true;
            }
        }
                
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
