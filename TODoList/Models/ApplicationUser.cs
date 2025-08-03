using Microsoft.AspNetCore.Identity;

namespace TODoList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ?Address { get; set; }
    }
}