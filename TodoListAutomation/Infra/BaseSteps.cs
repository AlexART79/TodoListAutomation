using TechTalk.SpecFlow;
using System.Threading;

namespace TodoListAutomation
{
    [Binding]
    public class BaseSteps
    {
        // shared data
        protected App app;

        [AfterStep]
        public void AfterStepDelay()
        {
            // For better visual debug purposes
            Thread.Sleep(500);
        }
    }    
}
