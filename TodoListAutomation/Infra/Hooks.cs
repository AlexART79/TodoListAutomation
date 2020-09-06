using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace TodoListAutomation
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer container;
        private ScenarioContext scenarioContext;

        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        public Hooks(IObjectContainer container, ScenarioContext sContext)
        {
            this.container = container;
            scenarioContext = sContext;
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
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

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter); 

            #endregion
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            #region ExtentReports
            // write report
            extent.Flush(); 
            #endregion
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            // extent reports
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {            
            // register injected value, autodisposable after test
            container.RegisterInstanceAs<App>(new App(), dispose:true);

            // extent reports
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep()
        {
            #region ExtentReports
            
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (scenarioContext.TestError == null)
            {
                scenario.CreateNode(new GherkinKeyword(stepType), ScenarioStepContext.Current.StepInfo.Text);
            }
            else
            {
                scenario.CreateNode(new GherkinKeyword(stepType), ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);

                // attach screenshots, logs

            }

            #endregion

            // For better visual debug purposes
            Thread.Sleep(500);
        }

        
    }
}
