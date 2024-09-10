using IUR_Backend.Models;
using IUR_Backend.Models.Dto.TeacherDto;
using IUR_Backend.Models.Service;
using IUR_Backend.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IUR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "UniAdministrator")]
    public class UniAdministratorController : ControllerBase
    {
        // Change the types from DTOs to actual entities
        private readonly UserManager<ApplicationUser> _userManager; // Use base User class or specific UniAdministrator if needed
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager; // Use the correct entity type
        private readonly AuthService _authService;

        public UniAdministratorController(UserManager<ApplicationUser> userManager, // Adjusted to the base User or specific type
                                          RoleManager<Role> roleManager,
                                          SignInManager<ApplicationUser> signInManager,
                                          AuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("register-teacher")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = new Teacher
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Creating the teacher using the UserManager
            var result = await _userManager.CreateAsync(teacher, model.Password);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Teacher creation failed", Errors = result.Errors });

            // Ensure the Teacher role exists
            var roleExists = await _roleManager.RoleExistsAsync("Teacher");
            if (!roleExists)
            {
                var role = new Role { Name = "Teacher" };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                    return StatusCode(500, new { Message = "Role creation failed", Errors = roleResult.Errors });
            }

            // Assign Teacher role to the new user
            var roleAssignmentResult = await _userManager.AddToRoleAsync(teacher, "Teacher");
            if (!roleAssignmentResult.Succeeded)
                return StatusCode(500, new { Message = "Failed to assign role to teacher", Errors = roleAssignmentResult.Errors });

            return Ok(new { Message = "Teacher registered successfully" });
        }

        // PUT: api/UniAdministrator/update-teacher
        [HttpPut("update-teacher")]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Valid teacher data is required.");
            }

            // Find the teacher by email
            var teacher = await _userManager.FindByEmailAsync(model.Email);
            if (teacher == null)
            {
                return NotFound("Teacher not found.");
            }

            // Update teacher properties
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;

            var updateResult = await _userManager.UpdateAsync(teacher);
            if (!updateResult.Succeeded)
            {
                return StatusCode(500, new { Message = "Failed to update teacher", Errors = updateResult.Errors });
            }

            return Ok(new { Message = "Teacher updated successfully" });
        }
        [HttpDelete("delete-teacher")]
        public async Task<IActionResult> DeleteTeacher([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            // Find the teacher by email
            var teacher = await _userManager.FindByEmailAsync(email);
            if (teacher == null)
            {
                return NotFound(new { Message = "Teacher not found." });
            }

            // Remove teacher
            var result = await _userManager.DeleteAsync(teacher);
            if (!result.Succeeded)
            {
                return StatusCode(500, new { Message = "Failed to delete teacher", Errors = result.Errors });
            }

            return Ok(new { Message = "Teacher deleted successfully" });
        }
    }
}