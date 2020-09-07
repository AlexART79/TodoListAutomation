using CommonClasses;
using DBFramework;
using RestFramework;
using System;
using WebUiFramework;

namespace TodoListAutomation {
  public class App : IDisposable {
    private bool _disposedValue;

    public readonly string ApiUrl = AutomationConfig.Instance.ApiHost;
    public readonly string AppUrl = AutomationConfig.Instance.BaseUrl;

    public Browser Browser { get; set; }
    public HomePage HomePage { get; set; }
    public TodoListDbClient TodoListDb { get; set; }
    public ApiClient TodoListApi { get; set; }

    public App() {
      TodoListDb = new TodoListDbClient();
      TodoListApi = new ApiClient(ApiUrl);
    }

    protected virtual void Dispose(bool disposing) {
      if (!_disposedValue) {
        if (disposing) {
          Close();
        }

        Browser = null;
        HomePage = null;
        TodoListDb = null;
        TodoListApi = null;

        _disposedValue = true;
      }
    }

    public void Dispose() {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    public void Close() {
      if (Browser != null) {
        Browser.Quit();
      }
    }
  }
}
