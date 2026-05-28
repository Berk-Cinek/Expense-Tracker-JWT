using EpenseTrackerAPI.Entities;
using EpenseTrackerAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EpenseTrackerAPI.Services
{
    public class AuthService(Data.AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(UserDTO request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        public async Task<User?> RegisterAsync(UserDTO request)
        {
            if(await context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(null!, request.Password);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                HashedPassword = hashedPassword
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
