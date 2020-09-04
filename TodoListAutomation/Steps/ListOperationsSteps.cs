using TechTalk.SpecFlow;
using NUnit.Framework;
using CommonClasses;
using System.Linq;
using System;

namespace TodoListAutomation
{    
    [Binding]
    public class ListOperationsSteps : BaseSteps
    {
        // to keep item data between steps
        TodoItem _item;  
        int _itemsCount;

        [AfterScenario]
        public void CleanScenario()
        {            
            if (_item != null)
            {
                app.TodoListApi.DeleteItem(_item.id);
            }
        }

        public ListOperationsSteps(App app)            
        {
            // shared data injection
            this.app = app;
        }

        [When(@"user clicked into new item input")]
        public void WhenUserClickedIntoNewItemInput()
        {
            app.HomePage.Input.Click();
        }

        [When(@"typed text ""(.*)""")]
        public void WhenTypedText(string text)
        {
            app.HomePage.Input.SendKeys(text);

            _item = new TodoItem() { text = text }; // save data for further use
        }

        [When(@"pressed Enter key")]
        public void WhenPressedEnterKey()
        {
            app.HomePage.Submit.Click();
        }

        [Then(@"new item should be added to the list")]
        public void ThenNewItemShouldBeAddedToTheList()
        {
            // 
            Func<TodoItem> findItem = () => {
                return app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);
            };

            Assert.That(Helpers.Wait(() => findItem() != null));

            // find item in the list
            var itemFound = findItem();
                        
            Assert.That(app.TodoListApi.GetAll().Count, Is.EqualTo(_itemsCount + 1));

            _item = itemFound; // save found item (all it's fields are required)
        }

        [Then(@"added item status should not be complete")]
        public void ThenAddedItemStatusShouldNotBeComplete()
        {
            Assert.That(!_item.complete);
        }

        [Given(@"todo list with few items is displayed")]
        public void GivenTodoListWithFewItemsIsDisplayed()
        {
            Assert.That(app.HomePage.List.Displayed);
            _itemsCount = app.TodoListApi.GetAll().Count();
        }

        [Given(@"there is an uncomplete item with text ""(.*)""")]
        public void GivenThereIsAnUncompleteItemWithText(string itemText)
        {            
            var item = new TodoItem() { text = itemText };
            
            app.TodoListApi.AddNew(item);

            item = app.TodoListApi.GetAll().FirstOrDefault(e => e.text == itemText);

            Assert.That(item, Is.Not.Null);
            Assert.That(!item.complete);

            _item = item; // save for further use
        }

        [Given(@"there is a completed item with text ""(.*)""")]
        public void GivenThereIsACompletedItemWithText(string itemText)
        {
            var item = new TodoItem() { text = itemText };

            app.TodoListApi.AddNew(item);

            item = app.TodoListApi.GetAll().FirstOrDefault(e => e.text == itemText);

            Assert.That(item, Is.Not.Null);
            Assert.That(item.complete);

            _item = item; // save for further use
        }

        [When(@"user click on item's left circle icon")]
        public void WhenUserClickOnItemSLeftCircleItem()
        {

        }

        [Then(@"item should be complete")]
        public void ThenItemShouldBeComplete()
        {

        }

        [Then(@"item should be not complete")]
        public void ThenItemShouldBeNotComplete()
        {

        }

        [When(@"user click on item's right X icon")]
        public void WhenUserClickOnItemSRightXIcon()
        {

        }

        [Then(@"item should be deleted")]
        public void ThenItemShouldBeDeleted()
        {

        }
    }
}
