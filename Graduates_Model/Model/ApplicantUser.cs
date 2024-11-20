using Graduates_Utility;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class ApplicantUser : IdentityUser
    {
        [Required]
        public int STUDENTID { get; set; }
        [Required]
        public StudentType STYDENTTYPE { get; set; }

        public string? email { get; set; }
    }
}
