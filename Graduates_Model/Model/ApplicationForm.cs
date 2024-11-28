using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class ApplicationForm
    {
        [Key]
        public int ID { get; set; }

        public string? YourName { get; set; }

        public string? YourEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }
    }
}