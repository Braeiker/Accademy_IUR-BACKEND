using IUR_Backend.Models;
using IUR_Backend.Models.Dto;
using IUR_Backend.Models.Service;
using IUR_Backend.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IUR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthService _authService;

        public ApplicationController(UserManager<ApplicationUser> userManager,
                                     RoleManager<Role> roleManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     AuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(student, model.Password);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Student creation failed", Errors = result.Errors });

            // Check if the role exists, and create it if not
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                var role = new Role { Name = "Student" };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                    return BadRequest(new { Message = "Role creation failed", Errors = roleResult.Errors });
            }

            await _userManager.AddToRoleAsync(student, "Student");

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDtoLogin model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var tokenResponse = await _authService.GenerateJwtAsync(model.Username);
                return Ok(new { Token = tokenResponse });
            }

            return Unauthorized(new { Message = "Invalid login attempt." });
        }
    }
}
