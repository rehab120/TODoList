using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODoList.IRepositry;
using TODoList.Models;
using TODoList.Services;

namespace TODoList.Repositry
{
    public class TaskRepositry : ITaskRepositry
    {
        private readonly ToDoListContext context;
        private readonly IConfiguration configuration;

        public TaskRepositry(ToDoListContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public List<Tassk> GetTasks()
        {
            return context.Tasks.OrderByDescending(s => s.Date).ToList();

        }

        public Tassk GetById(int id)
        {
            return context.Tasks.Include(I => I.user).FirstOrDefault(s => s.Id == id);
        }

        public void Add(Tassk task)
        {


            //DateTime dateTime = DateTime.Now;
            //if (task.Date.Date == dateTime.Date && task.IsDone == true)
            //{
            //    task.Status = Status.Completed;
            //}
            //else if (task.Date.Date == dateTime.Date && task.IsDone == false)
            //{
            //    task.Status = Status.TimeOver;

            //}
            //else
            //{
            //    task.Status = Status.Pending;
            //}

            Tassk Newtask = new Tassk();
            Newtask.Id = task.Id;
            Newtask.Date = task.Date;
            Newtask.IsDone = task.IsDone;
            Newtask.Status = task.Status;
            Newtask.Title = task.Title;
            Newtask.User_id = task.User_id;
            Newtask.Description = task.Description;
            context.Tasks.Add(Newtask);
            context.SaveChanges();
           // task.Status = IsCheckedStatus(Newtask.Id);
        }
        public void Update(Tassk task, int id)
        {
            var taskId = GetById(id);
            taskId.Title = task.Title;
            taskId.Description = task.Description;
            taskId.IsDone = task.IsDone;
            taskId.Date = task.Date;
            context.Tasks.Update(taskId);
            context.SaveChanges();
          //  taskId.Status = IsCheckedStatus(taskId.
               // Id);
        }
        public void Delete(int id)
        {

            var TaskId = GetById(id);
            if (TaskId != null)
            {
                context.Tasks.Remove(TaskId);
                context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException(nameof(TaskId));
            }
        }
        //public Status IsCheckedStatus(int id)
        //{
        //    DateTime now = DateTime.Now;
        //    var task = GetById(id);

        //    if (task.IsDone)
        //    {
        //        task.Status = Status.Completed;
        //    }
        //    else
        //    {

        //        if (task.Date > now)
        //        {
        //            task.Status = Status.Pending;
        //        }
        //        else
        //        {
        //            task.Status = Status.TimeOver;
        //        }
        //    }

        //    context.SaveChanges();
        //    return task.Status;
        //}


        public List<Tassk> Filtered(Status status)
        {
            var FilteredTask = context.Tasks.Where(s => s.Status == status).OrderByDescending(s => s.Date).ToList();
            return FilteredTask;
        }

        public void Finish(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                if (task.IsDone == false && task.Status == Status.Pending || task.IsDone == false && task.Status == Status.TimeOver || task.IsDone == false && task.Status == Status.Completed)
                {
                    task.Status = Status.Completed;
                    task.IsDone = true;
                }
                else
                {
                    Console.WriteLine("The Task is Already Finished");
                }
                context.Tasks.Update(task);
                context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException(nameof(task));
            }

        }

       

    }


    }


