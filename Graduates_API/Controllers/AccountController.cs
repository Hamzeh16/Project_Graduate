using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace TestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicantUser> userManager,
            IUnityofWork UnityofWork, IEmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _UnityofWork = UnityofWork;
            _roleManager = roleManager;
        }
        private readonly UserManager<ApplicantUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IUnityofWork _UnityofWork;

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IActionResult GetAllItems()
        //{
        //    List<ApplicantUser> objApplicantUserList = _UnityofWork.ApplicantRepositry.GetAll().ToList();
        //    return Ok(objApplicantUserList);
        //}

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] ApplicantDto registerUser, string role)
        {
            // Check For Email Is Not Empty
            if (string.IsNullOrEmpty(registerUser.ApplicantEmail))
            {
                throw new ArgumentException("Email address cannot be null or empty.");
            }

            if (ModelState.IsValid)
            {
                // Check User is Exist
                var UserExist = _userManager.FindByEmailAsync(registerUser.ApplicantEmail).Result;
                if (UserExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                                      new Respone() { Status = "Erorr", Message = "User already exists!" });
                }

                // Add User in database
                ApplicantUser appUser = new()
                {
                    UserName = registerUser.ApplicantName,
                    Email = registerUser.ApplicantEmail,
                    PhoneNumber = registerUser.ApplicantPhoneNumber,
                    //STUDENTID = registerUser.ApplicantIDNumber,
                    //STYDENTTYPE = registerUser.ApplicantType,
                };

                IdentityResult Result = new IdentityResult();

                if (IsStrongPasswords(registerUser.ApplicantPassword))
                {
                    Result = await _userManager.CreateAsync(appUser, registerUser.ApplicantPassword);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                                new Respone() { Status = "Erorr", Message = "The password must be at least 8 characters long, and include an uppercase letter, a lowercase letter, a number, and a special character!" });
                }

                // Assigen a Role
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if (!Result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                                     new Respone() { Status = "Erorr", Message = "User Faild To Created!" });
                    }
                    // Assigen a Role
                    await _userManager.AddToRoleAsync(appUser, role);

                    // Add Token To Verifiy Email
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    // Generate confirmation link
                    var ConfirmLinks = Url.Action("ConfirmEmail", "Authentication", new { token, email = appUser.Email }, Request.Scheme);
                    //var ConfirmLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = appUser.Email }, Request.Scheme);
                    //var ConfirmLink = $"{Request.Scheme}://{Request.Host}/Authentication/ConfirmEmail?token={token}&email={appUser.Email}";
                    var ConfirmLink = $"{Request.Scheme}://{Request.Host}/api/Account/ConfirmEmail?token={Uri.EscapeDataString(await token)}&email={Uri.EscapeDataString(appUser.Email)}";

                    //var ConfirmLink = $"{Request.Scheme}://{Request.Host}/ConfirmEmail?token={token}&email={appUser.Email}";
                    var message = new Messsage([appUser.Email], subject: "Confirm Email Link", content: ConfirmLink!);
                    _emailService.SendEmail(message);

                    return StatusCode(StatusCodes.Status201Created,
                                new Respone() { Status = "Succes", Message = $"User Created & Email Sent To {appUser.Email} Succesfully!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                new Respone() { Status = "Erorr", Message = "User Faild To Created!" });
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

        [HttpGet("SentEmail")]
        public IActionResult TestEmail()
        {
            var message =
                new Messsage(new string[]
                { "haaalaaaal@gmail.com" }, "Test", "<h1>Account is Created!</h1>");

            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
            new Respone() { Status = "Succes", Message = "Email Sent Succesfully!" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {

            // Check User is Exist
            var user = await _userManager.FindByEmailAsync(email);// erorr
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Respone { Status = "Succes", Message = "Email Verifiey Succesfully" });
                }
            }
 
            return StatusCode(StatusCodes.Status500InternalServerError,
                  new Respone() { Status = "Erorr", Message = "User Doesn't exists!" });
        }

        private bool IsStrongPasswords(string password)
        {
            // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
            var strongPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            return Regex.IsMatch(password, strongPasswordPattern);
        }
    }
}
