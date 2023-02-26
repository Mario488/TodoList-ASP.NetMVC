using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_list_app1.Infrastructure;
using todo_list_app1.Models;

namespace todo_list_app1.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext context;
        public TodoController(TodoContext context)
        {
            this.context = context;
        }
        //Get 
        public async Task<ActionResult> Index()
        {
            IQueryable<TodoList> items = from i in context.TodoList orderby i.Id select i;
            List<TodoList> todoList = await items.ToListAsync();
            return View(todoList);
        }
        //Get / todo/create
        public IActionResult Create() => View();
        //Post 
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been added!";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);
            if(item == null)
            {
                return NotFound(); 
            }
            return View(item);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been updated!";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.TodoList.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been deleted!";
            }
            return RedirectToAction("Index");
        }
    }
}
