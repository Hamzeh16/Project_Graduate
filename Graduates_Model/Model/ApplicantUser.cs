using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduates_Model.Model
{
    public class ApplicantUser : IdentityUser
    {
        public int? STUDENTID { get; set; }
        
        public string? APPLICANTTYPE { get; set; }

        public string? IMAGEURL { get; set; }

        public bool? REQUIST { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

    }
}
