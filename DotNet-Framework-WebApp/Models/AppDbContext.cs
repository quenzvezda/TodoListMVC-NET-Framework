using System.Data.Entity;

namespace DotNet_Framework_WebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Tire> Tires { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}