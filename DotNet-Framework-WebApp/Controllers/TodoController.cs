using System;
using System.Web.Mvc;
using DotNet_Framework_WebApp.Models;
using DotNet_Framework_WebApp.Services;

namespace DotNet_Framework_WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoService _todoService;

        public TodoController()
        {
            var context = new TodoItemContext();
            _todoService = new TodoService(context);
        }
        
        // GET: Todo
        public ActionResult Index()
        {
            var todoItems = _todoService.GetAllTodoItems();
            return View(todoItems);
        }

        // GET: Todo/PreAdd
        public ActionResult PreAdd()
        {
            return View();
        }

        // POST: Todo/Add
        [HttpPost]
        public ActionResult Add(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var newTodo = new TodoItem
                {
                    Title = title,
                    CreatedDate = DateTime.Now,
                    IsComplete = false
                };
                _todoService.AddTodoItem(newTodo);
            }
            return RedirectToAction("Index");
        }

        // GET: Todo/PreEdit/{id}
        public ActionResult PreEdit(int id)
        {
            var todoItem = _todoService.GetTodoById(id);
            if (todoItem == null) return HttpNotFound();

            return View(todoItem);
        }

        // POST: Todo/Edit
        [HttpPost]
        public ActionResult Edit(int id, string title, bool isComplete)
        {
            var todoItem = _todoService.GetTodoById(id);
            if (todoItem != null)
            {
                todoItem.Title = title;
                todoItem.IsComplete = isComplete;
                todoItem.UpdatedDate = DateTime.Now;

                if (isComplete)
                {
                    todoItem.FinishDate = DateTime.Now;
                }

                _todoService.UpdateTodoItem(todoItem);
            }

            return RedirectToAction("Index");
        }

        // GET: Todo/Delete/{id}
        public ActionResult Delete(int id)
        {
            _todoService.DeleteTodoItem(id);
            return RedirectToAction("Index");
        }
    }
}