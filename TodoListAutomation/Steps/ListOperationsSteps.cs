using CommonClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using TechTalk.SpecFlow;

namespace TodoListAutomation {
  [Binding]
  public class ListOperationsSteps {
    private App _app;

    // to keep item data between steps
    TodoItemData _item;
    int _itemsCount;

    [AfterScenario]
    public void CleanScenario() {
      if (_item != null) {
        var itemInfo = _app.TodoListApi.GetAll().FirstOrDefault(e => e.Text == _item.Text);
        if (itemInfo != null) {
          _app.TodoListApi.DeleteItem(itemInfo.Id);
          _item = null;
        }
      }
    }

    public ListOperationsSteps(App app) {
      // shared data injection
      this._app = app;
    }

    [When(@"typed text ""(.*)"" into new item form")]
    public void WhenTypedText(string text) {
      _app.HomePage.Form.Input
          .Click()
          .SendKeys(text);

      _item = new TodoItemData() { Text = text }; // save data for further use
    }

    [When(@"pressed Submit button")]
    public void WhenPressedEnterKey() {
      _app.HomePage.Form.Submit();
    }

    [Then(@"new item should be added to the list")]
    public void ThenNewItemShouldBeAddedToTheList() {
      Assert.That(
          Helpers.Wait(
              () => _app.HomePage.List.Items.Any(l => l.Text == _item.Text)
          )
      );

      // verify that count is greater
      Assert.That(_app.HomePage.List.Items.Count, Is.EqualTo(_itemsCount + 1));
    }

    [Then(@"added item status should not be complete")]
    public void ThenAddedItemStatusShouldNotBeComplete() {
      Assert.That(
          Helpers.Wait(
              () => _app.HomePage.List.Items.Any(l => l.Text == _item.Text && !l.Complete)
          )
      );
    }

    [Given(@"todo list with few items is displayed")]
    public void GivenTodoListWithFewItemsIsDisplayed() {
      Assert.That(_app.HomePage.List.Displayed);
      _itemsCount = _app.TodoListApi.GetAll().Count();
    }

    [Given(@"there is an uncomplete item with text ""(.*)""")]
    public void GivenThereIsAnUncompleteItemWithText(string itemText) {
      // add a complete item directly to DB
      _app.TodoListDb.Add(new TodoItemData() { Text = itemText, Complete = false });

      // verify it's not complete
      var item = _app.TodoListApi.GetAll().FirstOrDefault(e => e.Text == itemText);
      Assert.That(item, Is.Not.Null);
      Assert.That(!item.Complete);

      // save for further use
      _item = item;
    }

    [Given(@"there is a completed item with text ""(.*)""")]
    public void GivenThereIsACompletedItemWithText(string itemText) {
      // add a complete item directly to DB
      _app.TodoListDb.Add(new TodoItemData() { Text = itemText, Complete = true });

      // verify it's complete
      var item = _app.TodoListApi.GetAll().FirstOrDefault(e => e.Text == itemText);
      Assert.That(item, Is.Not.Null);
      Assert.That(item.Complete);

      // save for further use
      _item = item;
    }

    [When(@"user click on item's left circle icon")]
    public void WhenUserClickOnItemSLeftCircleItem() {
      var item = _app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.Text);
      Assert.That(item, Is.Not.Null, $"Item with text {_item.Text} is not in the list!");

      item.CheckIcon.Click();
    }

    [Then(@"item should be complete")]
    public void ThenItemShouldBeComplete() {
      // UI verification
      Assert.That(
          Helpers.Wait(
              () => _app.HomePage.List.Items.Any(l => l.Text == _item.Text && l.Complete)
          )
      );
    }

    [Then(@"item should be not complete")]
    public void ThenItemShouldBeNotComplete() {
      // UI verification
      Assert.That(
          Helpers.Wait(
              () => _app.HomePage.List.Items.Any(l => l.Text == _item.Text && !l.Complete)
          )
      );
    }

    [When(@"user click on item's right X icon")]
    public void WhenUserClickOnItemSRightXIcon() {
      var item = _app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.Text);
      Assert.That(item, Is.Not.Null, $"Item with text {_item.Text} is not in the list!");

      item.CloseIcon.Click();
    }

    [Then(@"item should be deleted")]
    public void ThenItemShouldBeDeleted() {

      // wait untill element deleted on the backend
      //Helpers.Wait(
      //    () => !_app.TodoListApi.GetAll().Any(l => l.Text == _item.Text)
      //);

      // UI verification      
      Assert.That(Helpers.Wait(() => !_app.HomePage.List.Items.Any(l => l.Exists && l.Text == _item.Text)));

      // set the _item to null to avoid removing nothing at the cleanup stage
      _item = null;
    }
  }
}

