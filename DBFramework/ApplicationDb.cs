using CommonClasses;
using Microsoft.EntityFrameworkCore;
using DBFramework.Properties;


namespace DBFramework
{
    public class ApplicationDb : DbContext
    {
        public DbSet<TodoItem> todo { get; set; }

        public ApplicationDb()
        {            
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            var host = Settings.Default.DB_HOST;
            var userId = Settings.Default.DB_USER;
            var password = Settings.Default.DB_PASSWORD;
            var databaseName = Settings.Default.DB_NAME;

            optionsBuilder.UseMySql($"server={host};UserId={userId};Password={password};database={databaseName};");
        }
    }
}
