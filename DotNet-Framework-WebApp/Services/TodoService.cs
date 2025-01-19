using System.Collections.Generic;
using System.Linq;
using DotNet_Framework_WebApp.Models;

namespace DotNet_Framework_WebApp.Services
{
    public class TodoService
    {
        private readonly TodoItemContext _context;

        public TodoService(TodoItemContext context)
        {
            _context = context;
        }
        
        public List<TodoItem> GetAllTodoItems()
        {
            return _context.TodoItems.ToList();
        }
        
        public TodoItem GetTodoById(int id)
        {
            return _context.TodoItems.Find(id);
        }

        public void AddTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public void UpdateTodoItem(TodoItem item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTodoItem(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}