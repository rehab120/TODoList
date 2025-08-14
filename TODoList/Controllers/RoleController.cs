using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TODoList.ViewModel;

namespace TODoList.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = roleViewModel.RoleName;
                IdentityResult identityResult = await roleManager.CreateAsync(identityRole);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {

                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
            return View(roleViewModel);
        }

    }
}
