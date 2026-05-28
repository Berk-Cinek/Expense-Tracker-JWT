using EpenseTrackerAPI.Data;
using EpenseTrackerAPI.Entities;
using EpenseTrackerAPI.Entities.Models;
using EpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IAuthService authService) : ControllerBase
    {
     
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var  user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("Username alread exists");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login (UserDTO request)
        {
            var token = await authService.LoginAsync(request);
            if (token == null) {
                return Unauthorized("Invalid Credentials");
            }
            return token;
            
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("you are authenticated");
        }
        
    }
}
