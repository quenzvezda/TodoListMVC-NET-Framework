using System;
using System.Linq;
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
            var context = new AppDbContext();
            _todoService = new TodoService(context);
        }
        
        public ActionResult Index()
        {
            using (var context = new AppDbContext())
            {
                return View(_todoService.GetAllTodoItems());
            }
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
        public ActionResult Edit(int id, string title, FormCollection form)
        {
            var todoItem = _todoService.GetTodoById(id);
            if (todoItem != null)
            {
                todoItem.Title = title;
                todoItem.IsComplete = form["isComplete"] == "on"; // Checkbox mengirimkan "on" jika dicentang
                todoItem.UpdatedDate = DateTime.Now;

                if (todoItem.IsComplete)
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