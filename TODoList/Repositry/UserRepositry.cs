using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODoList.IRepositry;
using TODoList.Migrations;
using TODoList.Models;
using TODoList.ViewModel;

namespace TODoList.Repositry
{
    public class UserRepositry : IUserRepositry
    {
        private readonly UserManager<ApplicationUser> userManger;
        ToDoListContext context;
        public UserRepositry(ToDoListContext context, UserManager<ApplicationUser> userManger)
        {
            this.context = context;
            this.userManger = userManger;
        }
        public async Task AddUserAsync(ApplicationUser user)
        {
            var User = new Users
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
            };
            context.Users.Add(User);
            await context.SaveChangesAsync();

        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await context.Users.Include(e=>e.Contact).ToListAsync();
        }

       
        public async Task<List<AdminRoleModelView>> GetAll()
        {
            var admins = await userManger.GetUsersInRoleAsync("Admin");

            var result = admins.Select(user => new AdminRoleModelView
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            }).ToList();

            
            return result;
        }

        public async Task Delete(string userId)
        {
            var user = await userManger.FindByIdAsync(userId);
            if (user != null)
            {
                await userManger.DeleteAsync(user);
            }
            else
            {
               throw new NullReferenceException("Id not Found");
            }
            

        }
        
        public async Task AddContact(Contact contact)
        {

            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
        }
        public async Task<List<Contact>> GetContacts()
        {
            return await context.Contacts.Include(e=>e.user).ToListAsync();
        }
        public async Task DeleteContact(int id)
        {
            var contactId = context.Contacts.FirstOrDefault(e => e.Id == id);   
            if (contactId != null)
            {
                context.Contacts.Remove(contactId);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException("Id not found");
            }
        }
      
  
    }
}
