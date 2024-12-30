using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduates_Service.Services.Dto
{
    // For Register
    public class ApplicantDto
    {
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public IFormFile? companyId { get; set; } // For file uploads    }
    }

    // For Login
    public class ApplicantLogin
    {
        public string? email { get; set; }

        public string? password { get; set; }
    }
}
