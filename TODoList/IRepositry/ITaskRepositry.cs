using TODoList.Models;

namespace TODoList.IRepositry
{
    public interface ITaskRepositry
    {
        List<Tassk> GetTasks();
        Tassk GetById(int id);
        void Add(Tassk task);
        void Update(Tassk task, int id);
        void Delete(int id);
        List<Tassk> Filtered(Status status);
        void Finish(int id);
    }
}
