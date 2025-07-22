using Microsoft.EntityFrameworkCore;

namespace TODoList.Models
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext() :base() { }
        public ToDoListContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Tassk> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
