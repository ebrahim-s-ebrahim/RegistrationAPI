using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProfileAPI.DTOs;
using ProfileAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // TODO: Validate the login request
            var result = await _userService.AuthenticateAsync(loginRequest);

            // TODO: Check if the user with the given email and password exists
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // TODO: Return the access token to the client
            return Ok(result);


        }
    }
}
