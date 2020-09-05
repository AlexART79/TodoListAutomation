using CommonClasses;
using DBFramework;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace TodoListAutomation
{
    [Binding]
    public class DbSteps
    {
        TodoListDbClient db;

        public DbSteps()            
        {
            db = new TodoListDbClient();
        }


        [When(@"user inserting new uncomplete item with text ""(.*)""")]
        public void WhenUserInsertingNewUncompleteItemWithText(string itemText)
        {
            db.Add(new TodoItemData() { text = itemText, complete = false });
        }

        [Then(@"DB should contain uncomplete item with text ""(.*)""")]
        public void ThenDBShouldContainUncompleteItemWithText(string itemText)
        {
            Assert.That(db.Items.Any(e => e.text == itemText && !e.complete));
        }

        [When(@"user updating item with text ""(.*)"" with new text ""(.*)""")]
        public void WhenUserUpdatingItemWithTextWithNewText(string oldText, string newText)
        {
            var item = db.Items.FirstOrDefault(e => e.text == oldText);

            if (item == null)
            {
                throw new Exception($"Todo item with text '{oldText}' was not found in DB");
            }

            item.text = newText;
            db.Update(item);
        }

        [When(@"user updating item with text ""(.*)"" - set complete to true")]
        public void WhenUserUpdatingItemWithText_SetCompleteToTrue(string itemText)
        {
            var item = db.Items.FirstOrDefault(e => e.text == itemText);

            if (item == null)
            {
                throw new Exception($"Todo item with text '{itemText}' was not found in DB");
            }

            item.complete = true;
            db.Update(item);
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

            db.Delete(item.id);
        }

        [Then(@"DB should not contain item with text ""(.*)""")]
        public void ThenDBShouldNotContainItemWithText(string itemText)
        {
            Assert.That(!db.Items.Any(e => e.text == itemText));
        }
    }
}
