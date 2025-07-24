using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODoList.IRepositry;
using TODoList.Models;

namespace TODoList.Repositry
{
    public class TaskRepositry : ITaskRepositry
    {
        private readonly ToDoListContext context;
        public TaskRepositry(ToDoListContext context)
        {
            this.context = context;
        }
        public List<Tassk> GetTasks() {
            var tasks = context.Tasks.Include(I=>I.user).ToList();
            return tasks;
        }

        public Tassk GetById(int id)
        {
            return context.Tasks.Include(I => I.user).FirstOrDefault(s=>s.Id == id);
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
            context.Tasks.Add(task);
            context.SaveChanges();
            task.Status = IsCheckedStatus(task.Id);
        }
        public void Update(Tassk task,int id)
        {
            var taskId = GetById(id);
            taskId.Title = task.Title;
            taskId.Description = task.Description;
            taskId.IsDone = task.IsDone;  
            taskId.Date = task.Date;
            context.Tasks.Update(taskId);
            context.SaveChanges();
            taskId.Status = IsCheckedStatus(taskId.
                Id);
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
        public Status IsCheckedStatus(int id)
        {
            DateTime dateTime = DateTime.Now;
            var TaskId = GetById(id);
           
            
                if (TaskId.Date.Date == dateTime.Date && TaskId.IsDone == true)
                {
                    TaskId.Status = Status.Completed;
                }
                else if(TaskId.Date.Date == dateTime.Date  && TaskId.IsDone == false)
                {
                    TaskId.Status = Status.TimeOver;
                   
                }
            else if (TaskId.Date.Date <= dateTime.Date.AddHours(-2) && TaskId.IsDone == false)
            {
                var email = new MailAddress("rehaabsayed1200@gmail.com", "Rehab");
                Console.WriteLine($"Warning: Task ID {id} is due soon. Notification sent to {email.Address}");
            }
            context.SaveChanges();
            return TaskId.Status;
            }

        public List<Tassk> Filtered(Status status)
        {
            var FilteredTask = context.Tasks.Where(s => s.Status == status).OrderByDescending(s => s.Date).ToList();
            return FilteredTask;
        }



     }






    }


