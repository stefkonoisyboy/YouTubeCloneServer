using Microsoft.AspNetCore.Mvc;
using YouTubeClone.Data.Models;
using YouTubeClone.Dtos.Users;
using YouTubeClone.Services.Interfaces;

namespace YouTubeClone.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        public IActionResult Register(RegisterDto dto)
        {
            if (this.usersService.Exists(dto.Email))
            {
                return this.BadRequest(new { Message = "User with that email already exists!", IsSuccessful = false });
            }

            var user = new ApplicationUser
            {
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            var result = this.usersService.Create(user);

            return this.Ok(new
            {
                Message = "You registered successfully!",
                IsSuccessful = true
            });
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            var user = this.usersService.GetByEmail(dto.Email);

            if (user == null)
            {
                return this.BadRequest(new { Message = "Invalid Credentials!", IsSuccessful = false });
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return this.BadRequest(new { Message = "Invalid Credentials!", IsSuccessful = false });
            }

            return this.Ok(new { Message = "You logged-in successfully!", IsSuccessful = true });
        }

        [HttpGet("{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            return this.Ok(this.usersService.GetByEmail(email));
        }
    }
}
