using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TODoList.IRepositry;
using TODoList.Models;

namespace TODoList.Controllers
{
    public class TaskController : Controller
    {
        ITaskRepositry taskRepositry;
        ToDoListContext context;
        public TaskController(ITaskRepositry taskRepositry, ToDoListContext context) 
        { 
            this.taskRepositry = taskRepositry;
            this.context = context; 

        }

        public IActionResult GetAll()
        {
            return View("GetAll", taskRepositry.GetTasks());
        }

        public IActionResult GetById(int id)
        {
            var task = taskRepositry.GetById(id);
            return View(task);
        }

        [HttpGet]
        public ActionResult Add()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Tassk task)
        {
            taskRepositry.Add(task);
            return RedirectToAction("GetAll");
        }
        public IActionResult Delete(int id)
        {
            taskRepositry.Delete(id);
            return RedirectToAction("GetAll");

        }
        //public ActionResult Edit(Tassk task) 
        //{
        //    taskRepositry.Update(task);
        //    return View();
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var authorE = context.Tasks.Include(s => s.user).FirstOrDefault(e => e.Id == id);
            return View(authorE);
        }
        [HttpPost]
        public ActionResult Edit(int id, Tassk task)
        {
            taskRepositry.Update(task,id);
            return RedirectToAction("GetById", new { Id = task.Id });
        }

        public IActionResult Filter(Status status)
        {
            taskRepositry.Filtered(status);
            return RedirectToAction("GetAll");
        }



    }
}
