﻿// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;

// namespace ProductManagementAPI.Helpers
// {
//     public class JwtHelper
//     {
//         private readonly string _key;

//         public JwtHelper(string key)
//         {
//             _key = key;
//         }

//         public string GenerateToken(int userId)
//         {
//             var tokenHandler = new JwtSecurityTokenHandler();
//             var key = Encoding.ASCII.GetBytes(_key);
//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(new Claim[]
//                 {
//                     new Claim(ClaimTypes.NameIdentifier, userId.ToString())
//                 }),
//                 Expires = DateTime.UtcNow.AddHours(1),
//                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//             };
//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             return tokenHandler.WriteToken(token);
//         }
//     }
// }
