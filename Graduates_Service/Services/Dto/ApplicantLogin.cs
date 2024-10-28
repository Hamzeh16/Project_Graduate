using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Service.Services.Dto
{
    public class ApplicantLogin
    {
        [Required]
        public string ApplicantUserName { get; set; }

        [Required]
        public string ApplicantPassword { get; set; }
    }
}
