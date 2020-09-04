using BoDi;
using TechTalk.SpecFlow;

namespace TodoListAutomation
{
    public class InitHelper
    {
        private readonly IObjectContainer container;

        public InitHelper(IObjectContainer container)
        {
            this.container = container;         
        }

        [BeforeScenario]
        public void Before()
        {
            // register injected value, autodisposable after test
            container.RegisterInstanceAs<App>(new App(), dispose:true);
        }
    }
}
