using CommonClasses;
using NUnit.Framework;
using System.Linq;
using TechTalk.SpecFlow;

namespace TodoListAutomation {
  [Binding]
  public class ListOperationsSteps {
    protected App app;

    // to keep item data between steps
    TodoItemData _item;
    int _itemsCount;

    [AfterScenario]
    public void CleanScenario() {
      if (_item != null) {
        var itemInfo = app.TodoListApi.GetAll().FirstOrDefault(e => e.text == _item.text);
        if (itemInfo != null) {
          app.TodoListApi.DeleteItem(itemInfo.id);
          _item = null;
        }
      }
    }

    public ListOperationsSteps(App app) {
      // shared data injection
      this.app = app;
    }

    [When(@"typed text ""(.*)"" into new item form")]
    public void WhenTypedText(string text) {
      app.HomePage.Form.Input
          .Click()
          .SendKeys(text);

      _item = new TodoItemData() { text = text }; // save data for further use
    }

    [When(@"pressed Submit button")]
    public void WhenPressedEnterKey() {
      app.HomePage.Form.Submit();
    }

    [Then(@"new item should be added to the list")]
    public void ThenNewItemShouldBeAddedToTheList() {
      Assert.That(
          Helpers.Wait(
              () => app.HomePage.List.Items.Any(l => l.Text == _item.text)
          )
      );

      // verify that count is greater
      Assert.That(app.HomePage.List.Items.Count, Is.EqualTo(_itemsCount + 1));
    }

    [Then(@"added item status should not be complete")]
    public void ThenAddedItemStatusShouldNotBeComplete() {
      Assert.That(
          Helpers.Wait(
              () => app.HomePage.List.Items.Any(l => l.Text == _item.text && !l.Complete)
          )
      );
    }

    [Given(@"todo list with few items is displayed")]
    public void GivenTodoListWithFewItemsIsDisplayed() {
      Assert.That(app.HomePage.List.Displayed);
      _itemsCount = app.TodoListApi.GetAll().Count();
    }

    [Given(@"there is an uncomplete item with text ""(.*)""")]
    public void GivenThereIsAnUncompleteItemWithText(string itemText) {
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
    public void GivenThereIsACompletedItemWithText(string itemText) {
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
    public void WhenUserClickOnItemSLeftCircleItem() {
      var item = app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.text);
      Assert.That(item, Is.Not.Null, $"Item with text {_item.text} is not in the list!");

      item.CheckIcon.Click();
    }

    [Then(@"item should be complete")]
    public void ThenItemShouldBeComplete() {
      // UI verification
      Assert.That(
          Helpers.Wait(
              () => app.HomePage.List.Items.Any(l => l.Text == _item.text && l.Complete)
          )
      );
    }

    [Then(@"item should be not complete")]
    public void ThenItemShouldBeNotComplete() {
      // UI verification
      Assert.That(
          Helpers.Wait(
              () => app.HomePage.List.Items.Any(l => l.Text == _item.text && !l.Complete)
          )
      );
    }

    [When(@"user click on item's right X icon")]
    public void WhenUserClickOnItemSRightXIcon() {
      var item = app.HomePage.List.Items.FirstOrDefault(e => e.Text == _item.text);
      Assert.That(item, Is.Not.Null, $"Item with text {_item.text} is not in the list!");

      item.CloseIcon.Click();
    }

    [Then(@"item should be deleted")]
    public void ThenItemShouldBeDeleted() {
      // UI verification
      Assert.That(
          Helpers.Wait(
              () => !app.HomePage.List.Items.Any(l => l.Text == _item.text)
          )
      );

      _item = null;
    }
  }
}

