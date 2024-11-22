using Graduates_Utility;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class ApplicantUser : IdentityUser
    {
        public int? STUDENTID { get; set; }
        
        public string? APPLICANTTYPE { get; set; }

        public string? IMAGEURL { get; set; }
    }
}
