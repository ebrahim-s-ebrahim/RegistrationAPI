using Microsoft.AspNetCore.Mvc;
using ProfileAPI.Data;
using ProfileAPI.Helpers;
using ProfileAPI.Models;
using ProfileAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly ProfileContext _context;
        private readonly EmailService _emailService;
        private readonly UserService _userService;
        private readonly CountryService _countryService;

        public RegisterationController(ProfileContext context, EmailService emailService, UserService userService, CountryService countryService)
        {
            _context = context;
            _emailService = emailService;
            _userService = userService;
            _countryService = countryService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Info request)
        {
            //Check validation of the provided info
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Load the countries from the JSON file and convert it to Country object
            List<Country> countries = await _countryService.LoadCountriesAsync();

            Country selectedCountry = CountryConverter.Convert(request.Country.Name, request.Country.DialCode, countries);

            // Validate country and dial code
            if (!ValidationHelper.IsValidCountry(selectedCountry.Name, countries) || !ValidationHelper.IsValidDialCode(selectedCountry.DialCode, countries))
            {
                return BadRequest("Invalid country or dial code.");
            }

            //// Send confirmation email and save 
            var verificationCode = _emailService.GenerateVerificationCode();
            var email = request.Email;
            _emailService.SendVerificationEmail(email, verificationCode);
            
            // Add registered info to database and save
            await _context.AddAsync(request);
            await _context.AddAsync(verificationCode);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            // Check if code matches confirmation code sent to user
            var user = await _userService.GetByEmailAsync(email);
            if (user == null || user.EmailConfirmCode != code)
            {
                return BadRequest("Invalid email or confirmation code");
            }

            // Update user's email status in database
            user.EmailIsChecked = true;
            user.EmailConfirmCode = null;
            await _userService.UpdateUserAsync(user);

            return Ok("Email confirmed successfully");
        }
    }
}
