using NUnit.Framework;
using TechTalk.SpecFlow;
using WebUiFramework;
using WebUiFramework.BrowserFactory;

namespace TodoListAutomation {
  [Binding]
  public class HomePageSteps {
    protected App app;

    public HomePageSteps(App app) {
      // shared data injection
      this.app = app;

      // init app
      this.app.Browser = BrowserFactory.GetBrowser();
      this.app.HomePage = new HomePage(this.app.Browser);
    }

    [Given(@"home page is loaded")]
    public void WhenHomePageIsLoaded() {
      // open app home page
      app.HomePage.Open(app.AppUrl);
    }

    [Then(@"header should be displayed")]
    public void ThenHeaderShouldBeDisplayed() {
      Assert.That(app.HomePage.Header.Displayed);
    }

    [Then(@"header should have text ""(.*)""")]
    public void ThenHeaderShouldHaveText(string headerText) {
      Assert.That(app.HomePage.Header.Text, Is.EqualTo(headerText));
    }

    [Then(@"todo list should be displayed")]
    public void ThenTodoListShouldBeDisplayed() {
      Assert.That(app.HomePage.List.Displayed);
    }

    [Then(@"add new item form should be displayed")]
    public void ThenAddNewItemFormShouldBeDisplayed() {
      Assert.That(app.HomePage.Form.Displayed);
    }
  }
}
