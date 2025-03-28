using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
        bool SaveChanges();
    }
}
