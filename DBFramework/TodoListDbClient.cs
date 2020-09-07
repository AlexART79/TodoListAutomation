using CommonClasses;
using System.Collections.Generic;
using System.Linq;

namespace DBFramework {
  public class TodoListDbClient {
    public TodoListDbClient() {

    }

    public List<TodoItemData> Items {
      get {
        using (TodoDbContext db = new TodoDbContext()) {
          return db.todo.ToList<TodoItemData>();
        }
      }
    }

    public void Add(TodoItemData item) {
      using (TodoDbContext db = new TodoDbContext()) {
        db.todo.Add(item);
        db.SaveChanges();
      }
    }

    // delete
    public void Delete(int id) {
      var del_item = Items.FirstOrDefault(e => e.id == id);

      if (del_item == null) {
        return;
      }

      using (TodoDbContext db = new TodoDbContext()) {
        // remove item and save changes
        db.todo.Remove(del_item);
        db.SaveChanges();
      }
    }

    // update
    public void Update(TodoItemData item) {
      var up_item = Items.FirstOrDefault(e => e.id == item.id);

      if (up_item == null) {
        return;
      }

      // update data in item
      up_item.text = item.text;
      up_item.complete = item.complete;

      using (TodoDbContext db = new TodoDbContext()) {
        // update item in the list and save changes
        db.todo.Update(up_item);
        db.SaveChanges();
      }
    }
  }
}
