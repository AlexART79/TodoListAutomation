using System.Collections.Generic;
using System.Linq;
using CommonClasses;


namespace DBFramework
{
    public class TodoList
    {
        public TodoList ()
        {
            
        }

        public List<TodoItem> Items
        {
            get
            {
                using (ApplicationDb db = new ApplicationDb())
                {
                    return db.todo.ToList<TodoItem>();
                }
            }
        }

        public void Add(TodoItem item)
        {
            using (ApplicationDb db = new ApplicationDb())
            {
                db.todo.Add(item);
                db.SaveChanges();
            }
        }                
    }
}
