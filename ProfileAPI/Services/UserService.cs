using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProfileAPI.Data;
using ProfileAPI.DTOs;
using ProfileAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProfileAPI.Services
{
    public class UserService
    {
        private readonly ProfileContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ProfileContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<Info> GetByEmailAsync(string email)
        {
            return await _context.Info.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Info> Create(Info info)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == info.Country.Name);
            if (country == null)
            {
                // If the country is not found in the database, create a new instance and add it to the context
                country = new Country { Name = info.Country.Name, DialCode = info.Country.DialCode };
                _context.Countries.Add(country);
            }

            var information = new Info
            {
                FirstName = info.FirstName,
                LastName = info.LastName,
                Country = country,
                PhoneNumber = info.PhoneNumber,
                Email = info.Email,
                EmailIsChecked = false,
                Password = info.Password
            };

            await _context.Info.AddAsync(info);
            await _context.SaveChangesAsync();

            return information;
        }

        public async Task<Info> UpdateUserAsync(Info user)
        {
            var existingUser = await _context.Info
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null)
            {
                throw new ArgumentException("User does not exist");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Country = user.Country;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Password = user.Password;
            existingUser.Photo = user.Photo;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            // Find user by email
            var user = await _context.Info.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);

            // Return null if user is not found
            if (user == null)
            {
                return null;
            }

            // Verify password
            if (user.Password != loginRequest.Password)
            {
                return null;
            }

            // Authentication successful, generate and return JWT token
            var token = GenerateJwtToken(user);
            var loginResponse = new LoginResponse
            {
                Email = user.Email,
                Token = token.ToString()
            };
            return loginResponse;
        }

        public Object GenerateJwtToken(Info user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSecret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = tokenHandler.WriteToken(token);

            return response;
        }
    }

}
