using NUnit.Framework;
using TechTalk.SpecFlow;
using WebUiFramework;
using WebUiFramework.BrowserFactory;

namespace TodoListAutomation {
  [Binding]
  public class HomePageSteps {
    private App _app;

    public HomePageSteps(App app) {
      // shared data injection
      _app = app;

      // init app
      _app.Browser = BrowserFactory.GetBrowser();
      _app.HomePage = new HomePage(_app.Browser);
    }

    [Given(@"home page is loaded")]
    public void WhenHomePageIsLoaded() {
      // open app home page
      _app.HomePage.Open(_app.AppUrl);
    }

    [Then(@"header should be displayed")]
    public void ThenHeaderShouldBeDisplayed() {
      Assert.That(_app.HomePage.Header.Displayed);
    }

    [Then(@"header should have text ""(.*)""")]
    public void ThenHeaderShouldHaveText(string headerText) {
      Assert.That(_app.HomePage.Header.Text, Is.EqualTo(headerText));
    }

    [Then(@"todo list should be displayed")]
    public void ThenTodoListShouldBeDisplayed() {
      Assert.That(_app.HomePage.List.Displayed);
    }

    [Then(@"add new item form should be displayed")]
    public void ThenAddNewItemFormShouldBeDisplayed() {
      Assert.That(_app.HomePage.Form.Displayed);
    }
  }
}
