using CommonClasses;
using System.Collections.Generic;
using System.Linq;

namespace DBFramework {
  public class TodoListDbClient {

    public List<TodoItemData> Items {
      get {
        using (TodoDbContext db = new TodoDbContext()) {
          return db.Todo.ToList<TodoItemData>();
        }
      }
    }

    public TodoListDbClient() {

    }

    public void Add(TodoItemData item) {
      using (TodoDbContext db = new TodoDbContext()) {
        db.Todo.Add(item);
        db.SaveChanges();
      }
    }

    // delete
    public void Delete(int id) {
      var del_item = Items.FirstOrDefault(e => e.Id == id);

      if (del_item == null) {
        return;
      }

      using (TodoDbContext db = new TodoDbContext()) {
        // remove item and save changes
        db.Todo.Remove(del_item);
        db.SaveChanges();
      }
    }

    // update
    public void Update(TodoItemData item) {
      var up_item = Items.FirstOrDefault(e => e.Id == item.Id);

      if (up_item == null) {
        return;
      }

      // update data in item
      up_item.Text = item.Text;
      up_item.Complete = item.Complete;

      using (TodoDbContext db = new TodoDbContext()) {
        // update item in the list and save changes
        db.Todo.Update(up_item);
        db.SaveChanges();
      }
    }
  }
}
