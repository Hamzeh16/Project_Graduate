using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class ApplicantUser : IdentityUser
    {
        [Required]
        public int STUDENTID { get; set; }
        [Required]
        public string? STYDENTTYPE { get; set; }
    }
}
