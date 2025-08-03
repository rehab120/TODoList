using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TODoList.Models;
using TODoList.ViewModel;

namespace TODoList.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManager;
        ToDoListContext context;

        public AuthController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, ToDoListContext context)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.context = context;
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
               
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerViewModel.UserName;
                applicationUser.Email = registerViewModel.Email;
                applicationUser.PasswordHash = registerViewModel.Password;
                IdentityResult result = await userManger.CreateAsync(applicationUser,registerViewModel.Password);
                if (result.Succeeded)
                {
                    await userManger.AddToRoleAsync(applicationUser, "User");
                    await signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("password", item.Description);
                    }
                }

            }
           

            return View(registerViewModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManger.FindByNameAsync(loginViewModel.UserName);
                if (applicationUser != null)
                {
                    bool found = await userManger.CheckPasswordAsync(applicationUser, loginViewModel.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(applicationUser, loginViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "User name or Password Wrong");

            }
            return View(loginViewModel);
        }

        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult RegisterAsAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsAdmin(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerViewModel.UserName;
                applicationUser.Email = registerViewModel.Email;
                applicationUser.PhoneNumber = registerViewModel.PhoneNumber;
                applicationUser.PasswordHash = registerViewModel.Password;
                applicationUser.Address = registerViewModel.Address;
                IdentityResult result = await userManger.CreateAsync(applicationUser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await userManger.AddToRoleAsync(applicationUser, "Admin");
                    await signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("password", item.Description);
                    }
                }

            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult ContactUs() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Email = contactViewModel.Email;
                contact.UserName = contactViewModel.UserName;
                contact.Subject = contactViewModel.Subject;
                contact.Message = contactViewModel.Message;
                context.Contacts.Add(contact);
                context.SaveChanges();
                //return View(contact);
                return View("Thanks");


            }
            return View(contactViewModel);

        }

    }
}
