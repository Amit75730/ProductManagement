using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using BCrypt.Net;
using ProductManagementAPI.Services.Interfaces;
using ProductManagementAPI.Repositories.Interfaces;

namespace ProductManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserResponseDTO? RegisterUser(UserRegisterDTO userDto)
        {
            if (_userRepository.GetUserByEmail(userDto.Email) != null)
            {
                return null; // Email already exists
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hashedPassword
            };

            _userRepository.AddUser(user);
            _userRepository.SaveChanges();

            return GenerateToken(user);
        }

        public UserResponseDTO? LoginUser(UserLoginDTO userDto)
        {
            var user = _userRepository.GetUserByEmail(userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            {
                return null; // Invalid credentials
            }

            return GenerateToken(user);
        }

        private UserResponseDTO GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserResponseDTO { Token = tokenHandler.WriteToken(token) };
        }
    }
}
