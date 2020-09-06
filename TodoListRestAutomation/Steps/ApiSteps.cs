using CommonClasses;
using DBFramework;
using NUnit.Framework;
using RestFramework;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace TodoListAutomation
{
    [Binding]
    public class ApiSteps
    {
        ApiClient api;
        TodoListDbClient db;

        TodoItemData _item;

        public ApiSteps()            
        {
            db = new TodoListDbClient();
            api = new ApiClient(AutomationConfig.Instance.ApiHost);

            _item = null;
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (_item != null)
            {
                db.Delete(_item.id);
            }
        }

        [When(@"user inserting new item with text ""(.*)""")]
        public void WhenUserInsertingNewUncompleteItemWithText(string itemText)
        {
            api.AddNew(new TodoItemData() { text = itemText });
        }

        [Then(@"DB should contain uncomplete item with text ""(.*)""")]
        public void ThenDBShouldContainUncompleteItemWithText(string itemText)
        {
            Assert.That(db.Items.Any(e => e.text == itemText && !e.complete));

            // save item data to cleanup later...
            _item = db.Items.First(e => e.text == itemText);
        }
                
        [When(@"user updating item with text ""(.*)"" - set complete to true")]
        public void WhenUserUpdatingItemWithText_SetCompleteToTrue(string itemText)
        {
            var item = db.Items.FirstOrDefault(e => e.text == itemText);
            if (item == null)
            {
                throw new Exception($"Todo item with text '{itemText}' was not found in DB");
            }

            api.CompleteItem(item.id);
        }

        [Then(@"DB should contain complete item with text ""(.*)""")]
        public void ThenDBShouldContainCompleteItemWithText(string itemText)
        {
            Assert.That(db.Items.Any(e => e.text == itemText && e.complete));
        }

        [When(@"user deleting item with text ""(.*)""")]
        public void WhenUserDeletingItemWithText(string itemText)
        {
            var item = db.Items.FirstOrDefault(e => e.text == itemText);
            if (item == null)
            {
                throw new Exception($"Todo item with text '{itemText}' was not found in DB");
            }

            api.DeleteItem(item.id);
        }

        [Then(@"DB should not contain item with text ""(.*)""")]
        public void ThenDBShouldNotContainItemWithText(string itemText)
        {
            Assert.That(!db.Items.Any(e => e.text == itemText));

            _item = null; // nothing to cleanup, db is already in required state...
        }
    }
}
