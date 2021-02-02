using System.Threading.Tasks;
using Example.Business.AdvancedObjects;
using Example.Business.Models;

namespace Example.Business.Interfaces
{
    public interface IUserHandlers
    {
        Task<User> GetUser(string name);
        Task DeleteUser(string name);
        Task AddUser(User user);
        Task UpdateUser(User user);
    }
}