using System.Threading.Tasks;
using Example.Business.Models;

namespace Example.Business.Interfaces
{
    public interface IDatabaseAccess
    {
        Task<User> GetUser(string name);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string name);
        Task Save();
    }
}