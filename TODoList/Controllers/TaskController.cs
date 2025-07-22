using Microsoft.AspNetCore.Mvc;
using TODoList.IRepositry;
using TODoList.Models;

namespace TODoList.Controllers
{
    public class TaskController : Controller
    {
        ITaskRepositry taskRepositry;
        public TaskController(ITaskRepositry taskRepositry) 
        { 
            this.taskRepositry = taskRepositry;

        }

        public IActionResult GetAll()
        {
            return View("GetAll", taskRepositry.GetTasks());
        }
        public IActionResult Add(Tassk task)
        {
            taskRepositry.Add(task);
            return View();
        }
        public IActionResult Delete(int id)
        {
            taskRepositry.Delete(id);
            return RedirectToAction("GetAll");

        }
            //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
