using OpenQA.Selenium;
using System.Collections.Generic;

namespace WebUiFramework.Elements {
  public class TodoListItem : WebElement {
    public TodoListItem(IWebDriver driver, By locator = null, IWebElement context = null)
        : base(driver, locator, context) { }

    public WebElement CheckIcon => new WebElement(driver, By.CssSelector(".circle"), Element);
    public WebElement CloseIcon => new WebElement(driver, By.CssSelector(".close"), Element);
  }

  public class TodoList : WebElement {
    public TodoList(IWebDriver driver, By locator = null, IWebElement context = null)
        : base(driver, locator, context) { }

    public List<TodoListItem> Items {
      get {
        var lst = new List<TodoListItem>();

        // get all list item elements inside the list
        var elements = Element.FindElements(By.CssSelector(".todo-list-item"));

        // for each found element:
        foreach (var e in elements) {
          // wrap found elements with WebElement
          var we = new TodoListItem(driver).Wrap(e);
          lst.Add((TodoListItem)we);
        }

        return lst;
      }
    }
  }
}
