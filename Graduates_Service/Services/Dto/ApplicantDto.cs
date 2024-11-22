using Graduates_Utility;
using System.ComponentModel.DataAnnotations;

namespace Graduates_Service.Services.Dto
{
    // For Register
    public class ApplicantDto
    {
        public string? ApplicantName { get; set; }
        public int? ApplicantIDNumber { get; set; }
        public string? ApplicantPhoneNumber { get; set; }
        public string? ApplicantEmail { get; set; }
        public string? ApplicantPassword { get; set; }
        public string? ApplicantType { get; set; }
        public string? ImageUrl { get; set; }
    }
    // For Login
    public class ApplicantLogin
    {
        [Required]
        public string? ApplicantUserName { get; set; }

        [Required]
        public string? ApplicantPassword { get; set; }
    }
}
