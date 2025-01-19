using System.Data.Entity;

namespace DotNet_Framework_WebApp.Models
{
    public class TodoItemContext : DbContext
    {
        public TodoItemContext() : base("TodoItemContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        // DbSet untuk tabel TodoItems
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}