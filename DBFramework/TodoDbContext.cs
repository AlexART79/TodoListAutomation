using CommonClasses;
using Microsoft.EntityFrameworkCore;


namespace DBFramework {
  public class TodoDbContext : DbContext {
    public DbSet<TodoItemData> todo { get; set; }

    public TodoDbContext() {
      Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      var host = AutomationConfig.Instance.DbHost;
      var userId = AutomationConfig.Instance.DbUser;
      var password = AutomationConfig.Instance.DbPassword;
      var databaseName = AutomationConfig.Instance.DbName;

      optionsBuilder.UseMySql($"server={host};UserId={userId};Password={password};database={databaseName};");
    }
  }
}
