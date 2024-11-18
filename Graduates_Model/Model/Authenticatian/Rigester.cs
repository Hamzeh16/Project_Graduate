using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model.Authenticatian
{
    public class Rigester
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string? USERNAME { get; set; }
        
        
        [Required(ErrorMessage = "Email is Required")]
        public string? EMAIL { get; set; }
        

        [Required(ErrorMessage = "Password is Required")]
        public string? PASSWORD { get; set; }
    }
}
