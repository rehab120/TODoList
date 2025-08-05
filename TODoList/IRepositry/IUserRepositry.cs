using TODoList.Models;
using TODoList.ViewModel;

namespace TODoList.IRepositry
{
    public interface IUserRepositry
    {
        Task AddUserAsync(ApplicationUser user);
        Task<List<Users>> GetAllUsers();
        // Task<ApplicationUser> GetAllAdmins();
        Task<List<AdminRoleModelView>> GetAll();
        Task Delete(string userId);
        Task AddContact(Contact contact);
        Task<List<Contact>> GetContacts();
        Task DeleteContact(int id);

    }
}
