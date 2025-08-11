using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODoList.Models;

namespace TODoList.Services
{
    public class TaskChecker
    {
        
            private readonly IConfiguration _configuration;
            private readonly ToDoListContext context;
  
            public TaskChecker(IConfiguration configuration, ToDoListContext context)
            {
                _configuration = configuration;
                 this.context = context;
            }

        //public void CheckTasks()
        //{
        //    DateTime now = DateTime.Now;

        //    var tasks = context.Tasks
        //        .Include(t => t.user)
        //        .Where(t => !t.IsDone && !t.IsEmailSent) // فلترة من قاعدة البيانات
        //    .ToList();



        //    foreach (var task in tasks)
        //    {

        //        TimeSpan timeLeft = task.Date - now;

        //        if (timeLeft.TotalHours <= 2 && timeLeft.TotalHours > 0)
        //        {
        //            string senderName = "To Do List";
        //            string senderEmail = "rehaabsayed1200@gmail.com";
        //            string username = task.user.UserName;
        //            string email = task.user.Email;
        //            string subject = "Task Reminder";
        //            string message = $"Dear {username},\n\nYour task '{task.Title}' will end in less than 2 hours.";

        //            var emailSender = new EmailSender(_configuration);
        //            emailSender.SendEmail(senderName, senderEmail, username, email, subject, message);

        //            task.Status = Status.Pending;
        //            task.IsEmailSent = true; // علشان مايبعتش تاني
        //        }
        //    }

        //    // هنا لازم الحفظ
        //    context.SaveChanges();
        //}
        public void CheckTasks()
        {
            DateTime now = DateTime.Now;

            var tasks = context.Tasks
                .Include(t => t.user)
                .ToList();

            foreach (var task in tasks)
            {
                // ✅ تحديث الحالة لو المهمة منتهية
                if (task.IsDone)
                {
                    task.Status = Status.Completed;
                    continue; // ما نكملش باقي الفحص
                }

                // لو باقي أقل من ساعتين ولسه ما تبعتش الإيميل
                if (!task.IsEmailSent)
                {
                    TimeSpan timeLeft = task.Date - now;

                    if (timeLeft.TotalHours <= 2 && timeLeft.TotalHours > 0)
                    {
                        string senderName = "To Do List";
                        string senderEmail = "rehaabsayed1200@gmail.com";
                        string username = task.user.UserName;
                        string email = task.user.Email;
                        string subject = "Task Reminder";
                        string message = $"Dear {username},\n\nYour task '{task.Title}' will end in less than 2 hours.";

                        var emailSender = new EmailSender(_configuration);
                        emailSender.SendEmail(senderName, senderEmail, username, email, subject, message);

                        task.Status = Status.Pending;
                        task.IsEmailSent = true; // علشان مايبعتش تاني
                    }
                }

                // ✅ تحديث الحالة بناءً على الوقت
                if (task.Date > now)
                {
                    task.Status = Status.Pending;
                }
                else
                {
                    task.Status = Status.TimeOver;
                }
            }

            context.SaveChanges();
        }



    }
}
