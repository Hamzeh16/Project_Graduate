using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace TestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicantUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        private readonly UserManager<ApplicantUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var Items = await _userManager.Users.ToListAsync();
            return Ok(Items);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(ApplicantDto registerUser)
        {
            if (ModelState.IsValid)
            {
                ApplicantUser appUser = new()
                {
                    UserName = registerUser.ApplicantName,
                    Email = registerUser.ApplicantEmail,
                    PhoneNumber = registerUser.ApplicantPhoneNumber,
                    STUDENTID = registerUser.ApplicantIDNumber,
                    STYDENTTYPE = registerUser.ApplicantType,
                };

                IdentityResult Result = new IdentityResult();

                if (IsStrongPasswords(registerUser.ApplicantPassword))
                {
                     Result = await _userManager.CreateAsync(appUser, registerUser.ApplicantPassword);
                }
                else
                {
                    return Ok("The password must be at least 8 characters long, and include an uppercase letter, a lowercase letter, a number, and a special character.");
                }

                if (Result.Succeeded)
                {
                    return Ok("Succes");
                }
                else
                {
                    foreach (var item in Result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(ApplicantLogin loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicantUser? user = await _userManager.FindByNameAsync(loginUser.ApplicantUserName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, loginUser.ApplicantPassword))
                    {
                        return Ok("Sucsess");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invald User Name");
                }
            }
            return BadRequest();
        }

        private bool IsStrongPasswords(string password)
        {
            // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
            var strongPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            return Regex.IsMatch(password, strongPasswordPattern);
        }
    }
}
