using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TODoList.IRepositry;
using TODoList.Models;
using TODoList.ViewModel;

namespace TODoList.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepositry userRepositry;
        public UserController(IUserRepositry userRepositry) 
        { 
            this.userRepositry = userRepositry;
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
        public IActionResult ContactUs(ContactViewModel contactViewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);


            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Email = contactViewModel.Email;
                contact.UserName = userName;
                contact.Subject = contactViewModel.Subject;
                contact.Message = contactViewModel.Message;
                contact.User_id = userId;
                userRepositry.AddContact(contact);
                return View("Thanks");


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
