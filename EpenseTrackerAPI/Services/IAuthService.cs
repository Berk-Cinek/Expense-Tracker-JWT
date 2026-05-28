using EpenseTrackerAPI.Entities;
using EpenseTrackerAPI.Entities.Models;

namespace EpenseTrackerAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
    }
}
