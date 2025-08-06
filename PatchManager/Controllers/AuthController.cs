using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchManager.Data;
using PatchManager.Models;
using PatchManager.Services;
using System.Linq;

namespace PatchManager.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly PatchDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(PatchDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login/admin")]
        public IActionResult AdminLogin([FromBody] LoginDto dto)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == dto.Username);
            if (admin == null || !_authService.VerifyPassword(dto.Password, admin.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(admin.Username, "Admin");
            return Ok(new
            {
                token = token,
                customerId = 0
            });
        }

        [HttpPost("login/customer")]
        public IActionResult CustomerLogin([FromBody] LoginDto dto)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Username == dto.Username);
            if (customer == null || !_authService.VerifyPassword(dto.Password, customer.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(customer.Username, "Customer");
            return Ok( new
            {
                token = token,
                customerId = customer.Id
            });
        }

        [HttpPost("register/admin")]
        public IActionResult RegisterAdmin([FromBody] RegisterDto dto)
        {
            if (_context.Admins.Any(a => a.Username == dto.Username))
                return BadRequest("Admin already exists.");

            var hash = _authService.HashPassword(dto.Password);
            var admin = new Admin { Username = dto.Username, PasswordHash = hash };
            _context.Admins.Add(admin);
            _context.SaveChanges();

            return Ok("Admin registered successfully");
        }

        [HttpPost("register/customer")]
        public IActionResult RegisterCustomer([FromBody] RegisterDto dto)
        {
            if (_context.Customers.Any(c => c.Username == dto.Username))
                return BadRequest("Customer already exists.");

            var hash = _authService.HashPassword(dto.Password);
            var customer = new Customer { Username = dto.Username, PasswordHash = hash };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok("Customer registered successfully");
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
