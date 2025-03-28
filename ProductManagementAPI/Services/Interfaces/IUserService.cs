using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        UserResponseDTO? RegisterUser(UserRegisterDTO userDto);
        UserResponseDTO? LoginUser(UserLoginDTO userDto);
    }
}
