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
            ViewData["Status"] = "All";
            return View("GetAll", taskRepositry.GetTasks());
        }

        //public IActionResult GetById(int id)
        //{
        //    var task = taskRepositry.GetById(id);
        //    return View(task);
        //}

        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(Tassk task)
        {
            if (!ModelState.IsValid)
            {
                return View(task); 
            }
            taskRepositry.Add(task);
            return RedirectToAction("GetAll");
        }
        public IActionResult Delete(int id)
        {
            taskRepositry.Delete(id);
            return RedirectToAction("GetAll");

        }

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
            return RedirectToAction("GetAll", new { Id = task.Id });
        }

        public IActionResult Filter(string status)
        {
            ViewData["Status"] = status;
            

            if (string.IsNullOrEmpty(status) || status == "All")
            {
                var allTasks = taskRepositry.GetTasks(); 
                return View("GetAll", allTasks); 
            }

            if (Enum.TryParse<Status>(status, out var parsedStatus))
            {
                var taskFiltered = taskRepositry.Filtered(parsedStatus);
                return View("GetAll", taskFiltered);
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Finished(int id)
        {
            taskRepositry.Finish(id);
            return View();
        }



    }
}
