using Allure.Commons;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace TodoListAutomation {
  [Binding]
  public class Hooks {
    private readonly IObjectContainer _container;
    private ScenarioContext _scenarioContext;

    private static ExtentTest _featureName;
    private static ExtentTest _scenario;
    private static ExtentReports _extent;

    private App _app;

    public Hooks(IObjectContainer container, ScenarioContext sContext) {
      _container = container;
      _scenarioContext = sContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun() {
      #region ExtentReports

      // Init extent reports
      var htmlReporter = new ExtentHtmlReporter(
          Path.Combine(
              Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
              "Reports",
              "ExtentReport.html"
          )
      );

      htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
      htmlReporter.Config.EnableTimeline = true;

      _extent = new ExtentReports();
      _extent.AttachReporter(htmlReporter);

      #endregion
    }

    [AfterTestRun]
    public static void AfterTestRun() {
      #region ExtentReports
      // write report
      _extent.Flush();
      #endregion
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext) {
      // extent reports
      _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void BeforeScenario() {
      _app = new App();

      // register injected value, autodisposable after test
      _container.RegisterInstanceAs<App>(_app, dispose: true);

      // extent reports
      _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
    }

    [AfterStep]
    public void AfterStep() {
      #region ExtentReports

      var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

      if (_scenarioContext.TestError == null) {
        _scenario.CreateNode(new GherkinKeyword(stepType), ScenarioStepContext.Current.StepInfo.Text);
      } else {
        _scenario.CreateNode(new GherkinKeyword(stepType), ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);

        // attach screenshots, logs

      }

      #endregion

      #region Allure

      // attach scr on fail
      if (_scenarioContext.TestError != null) {
        var scr = _app.Browser.TakeScreenshot();
        AllureLifecycle.Instance.AddAttachment("App state on failure", "image/png", scr);
      }

      #endregion


      // For better visual debug purposes
      Thread.Sleep(500);
    }


  }
}
