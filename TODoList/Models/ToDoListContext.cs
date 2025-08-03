using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TODoList.Models
{
    public class ToDoListContext : IdentityDbContext<ApplicationUser>
    {
        public ToDoListContext() :base() { }
        public ToDoListContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Tassk> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
