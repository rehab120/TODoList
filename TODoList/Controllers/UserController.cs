using System.Security.Claims;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TODoList.IRepositry;
using TODoList.Models;
using TODoList.Services;
using TODoList.ViewModel;

namespace TODoList.Controllers
{
    public class UserController : Controller

    {
        private readonly IConfiguration configuration;
        private readonly IUserRepositry userRepositry;
        public UserController(IUserRepositry userRepositry, IConfiguration configuration) 
        { 
            this.userRepositry = userRepositry;
            this.configuration = configuration;
        }

        public async Task<IActionResult> GetAll()
        {
           var admins = await userRepositry.GetAll();
            return View(admins);
        }

        public async Task<IActionResult> GetAllUser()
        {
            var users = await userRepositry.GetAllUsers(); 
            return View(users);
        }

     
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await userRepositry.Delete(id);
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactViewModel contactViewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);


            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Email = email;
                contact.UserName = userName;
                contact.Subject = contactViewModel.Subject;
                contact.Message = contactViewModel.Message;
                contact.User_id = userId;
                await userRepositry.AddContact(contact);
                BackgroundJob.Enqueue<EmailSender>(x => x.SendEmail(
                    "To Do List",
                    "rehaabsayed1200@gmail.com",
                    userName,
                    email,
                    "Regarding Your Contact Submission",
                    $"Dear {userName},\n\nThank you for reaching out to us. We have received your message and our team will get back to you shortly.\n\nBest regards,\nThe To Do List Team"
                ));

                return RedirectToAction("Index", "Home");


            }
            return View(contactViewModel);

        }

        public async Task<IActionResult> ContactFromAgent()
        {
            var contacts = await userRepositry.GetContacts();
            return View(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteContacts(int Id)
        {
            await userRepositry.DeleteContact(Id);
            return RedirectToAction("ContactFromAgent");
        }

  
        
    }
}
