using Graduates_Data.Data;
using Graduates_Model.Model;
using Graduates_Service.Services.Dto;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace TestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicantUser> userManager,
            IUnityofWork UnityofWork, IEmailService emailService,
            RoleManager<IdentityRole> roleManager, IConfiguration config, AppDbContext db)
        {
            _userManager = userManager;
            _emailService = emailService;
            _UnityofWork = UnityofWork;
            _roleManager = roleManager;
            _config = config;
            _db = db;
        }
        private readonly UserManager<ApplicantUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IUnityofWork _UnityofWork;
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromForm] ApplicantDto registerUser)
        {
            // Check For Email Is Not Empty
            if (string.IsNullOrEmpty(registerUser.Email))
            {
                throw new ArgumentException("Email address cannot be null or empty.");
            }

            if (ModelState.IsValid)
            {
                // Check User is Exist
                var UserExist = _userManager.FindByEmailAsync(registerUser.Email).Result;
                if (UserExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                                      new Respone() { Status = "Erorr", Message = "User already exists!" });
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                // التأكد من أن المجلد موجود
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // تحديد المسار الذي سيتم تخزين السيرة الذاتية فيه
                var filePath = Path.Combine(uploadsFolder, registerUser.CompanyId.FileName);

                // تخزين الملف في المجلد
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await registerUser.CompanyId.CopyToAsync(stream);  // نسخ محتويات الملف إلى المجلد
                }

                // Add User in database
                ApplicantUser appUser = new()
                {
                    UserName = registerUser.Name,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.Phone,
                    //STUDENTID = registerUser.id,
                    APPLICANTTYPE = registerUser.Role,
                    REQUIST = true,
                    IMAGEURL = filePath
                };

                // Check If Company
                if (appUser.APPLICANTTYPE == "Company")
                {
                    appUser.REQUIST = null;
                }

                IdentityResult Result = new IdentityResult();

                if (IsStrongPasswords(registerUser.Password))
                {
                    Result = await _userManager.CreateAsync(appUser, registerUser.Password);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                                new Respone() { Status = "Erorr", Message = "The password must be at least 8 characters long, and include an uppercase letter, a lowercase letter, a number, and a special character!" });
                }

                // Assigen a Role
                if (await _roleManager.RoleExistsAsync(appUser.APPLICANTTYPE))
                {
                    if (!Result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                                     new Respone() { Status = "Erorr", Message = "User Faild To Created!" });
                    }

                    // Assigen a Role
                    await _userManager.AddToRoleAsync(appUser, appUser.APPLICANTTYPE);

                    // Add Token To Verifiy Email
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                    // Generate confirmation link
                    var ConfirmLinks = Url.Action("ConfirmEmail", "Authentication", new { token, email = appUser.Email }, Request.Scheme);
                    var ConfirmLink = $"{Request.Scheme}://{Request.Host}/api/Account/ConfirmEmail?token={Uri.EscapeDataString(await token)}&email={Uri.EscapeDataString(appUser.Email)}";

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

        //[HttpPost("login")]
        //public async Task<IActionResult> LoginUser([FromBody] ApplicantLogin loginUser)
        //{
        //    ApplicantUser? user = await _userManager.FindByEmailAsync(loginUser.email);

        //    if (user?.REQUIST == true || user?.REQUIST == null)
        //    {
        //        if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.password))
        //        {
        //            var authClaim = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name,user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };
        //            var userRole = await _userManager.GetRolesAsync(user);
        //            foreach (var role in userRole)
        //            {
        //                authClaim.Add(new Claim(ClaimTypes.Role, role));
        //            }

        //            var JwtToken = GetToken(authClaim);

        //            return Ok(new
        //            {
        //                token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
        //                expiration = JwtToken.ValidTo
        //            });
        //        }
        //        return StatusCode(StatusCodes.Status403Forbidden,
        //               new Respone() { Status = "Erorr", Message = "Company Is Checked!" });

        //    }
        //    return Unauthorized();
        //}

        //private JwtSecurityToken GetToken(List<Claim> authClaim)
        //{
        //    var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

        //    var token = new JwtSecurityToken(
        //        issuer: _config["JWT:ValidIssuer"],
        //        audience: _config["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddHours(1),
        //        claims: authClaim,
        //        signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256));

        //    return token;
        //}


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] ApplicantLogin loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginUser.password)))
            {
                return Unauthorized(new { Status = "Error", Message = "Invalid email or password" });
            }

            if (user.REQUIST == false || user.REQUIST == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new { Status = "Error", Message = "Account is pending approval or not active" });
            }

            var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var JwtToken = GetToken(authClaim);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                expiration = JwtToken.ValidTo,
                role = userRole.FirstOrDefault() // إضافة الدور إلى الاستجابة
            });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
            );
        }

        // Approve a post request
        [HttpPatch("PendingPostRequests")]
        public IActionResult PendingPostRequests(ApplicantUser applicantUser, string action, string? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            applicantUser = _UnityofWork.ApplicantRepositry.Get(c => c.Id == ID);
            if (applicantUser != null)
            {
                if (action == "approve")
                {
                    // Handle approve logic
                    applicantUser.REQUIST = true;
                }
                else if (action == "reject")
                {
                    // Handle reject logic
                    applicantUser.REQUIST = false;
                }
                _UnityofWork.ApplicantRepositry.Update(applicantUser);
                _UnityofWork.Save();

                // Sent Approved Email *******
                SentEmail(applicantUser.Email);
            }
            return RedirectToAction("PendingPostRequests");
        }

        [HttpGet("SentEmail")]
        public IActionResult SentEmail(string email)
        {
            var message =
                new Messsage(new string[]
                { email }, "Confirm", "Account is Approved!");

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