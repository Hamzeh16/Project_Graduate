using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
