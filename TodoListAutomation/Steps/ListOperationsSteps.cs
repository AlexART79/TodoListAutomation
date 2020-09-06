using TechTalk.SpecFlow;
using NUnit.Framework;
using CommonClasses;
using System.Linq;
using System;

namespace TodoListAutomation
{    
    [Binding]
    public class ListOperationsSteps
    {
        protected App app;

        // to keep item data between steps
        TodoItemData _item;  
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

        [When(@"typed text ""(.*)"" into new item form")]
        public void WhenTypedText(string text)
        {
            app.HomePage.Form.Input
                .Click()
                .SendKeys(text);

            _item = new TodoItemData() { text = text }; // save data for further use
        }

        [When(@"pressed Submit button")]
        public void WhenPressedEnterKey()
        {
            app.HomePage.Form.Submit();
        }

        [Then(@"new item should be added to the list")]
        public void ThenNewItemShouldBeAddedToTheList()
        {
            // utility func
            Func<TodoItemData> findItem = () =>
                app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);
                     
            Assert.That(Helpers.Wait(() => findItem() != null));

            // find item in the list
            var itemFound = findItem();
            _item = itemFound; // save found item (all it's fields are required)

            Assert.That(app.TodoListApi.GetAll().Count, Is.EqualTo(_itemsCount + 2));
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
            // add a complete item directly to DB
            app.TodoListDb.Add(new TodoItemData() { text = itemText, complete = false });

            // verify it's not complete
            var item = app.TodoListApi.GetAll().FirstOrDefault(e => e.text == itemText);
            Assert.That(item, Is.Not.Null);
            Assert.That(!item.complete);

            // save for further use
            _item = item;
        }

        [Given(@"there is a completed item with text ""(.*)""")]
        public void GivenThereIsACompletedItemWithText(string itemText)
        {
            // add a complete item directly to DB
            app.TodoListDb.Add(new TodoItemData() { text = itemText, complete = true });

            // verify it's complete
            var item = app.TodoListApi.GetAll().FirstOrDefault(e => e.text == itemText);
            Assert.That(item, Is.Not.Null);
            Assert.That(item.complete);

            // save for further use
            _item = item; 
        }

        [When(@"user click on item's left circle icon")]
        public void WhenUserClickOnItemSLeftCircleItem()
        {
            var item = app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.text);
            Assert.That(item, Is.Not.Null, $"Item with text {_item.text} is not in the list!");

            item.CheckIcon.Click();
        }

        [Then(@"item should be complete")]
        public void ThenItemShouldBeComplete()
        {            
            // utility func
            Func<TodoItemData> findItem = () =>
                app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);

            Assert.That(Helpers.Wait(() =>
            {
                var item = findItem();
                return item != null && item.complete;
            }));
        }

        [Then(@"item should be not complete")]
        public void ThenItemShouldBeNotComplete()
        {
            // utility func
            Func<TodoItemData> findItem = () =>
                app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);

            Assert.That(Helpers.Wait(() =>
            {
                var item = findItem();
                return item != null && !item.complete;
            }));
        }

        [When(@"user click on item's right X icon")]
        public void WhenUserClickOnItemSRightXIcon()
        {
            var item = app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.text);
            Assert.That(item, Is.Not.Null, $"Item with text {_item.text} is not in the list!");

            item.CloseIcon.Click();
        }

        [Then(@"item should be deleted")]
        public void ThenItemShouldBeDeleted()
        {
            // utility func
            Func<TodoItemData> findItem = () =>
                app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);

            Assert.That(Helpers.Wait(() => findItem() == null));
        }
    }
}
