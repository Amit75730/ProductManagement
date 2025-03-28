using System.Threading.Tasks;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUser(UserRegisterDTO userDto);
        Task<string> LoginUser(UserLoginDTO userDto);
    }
}
