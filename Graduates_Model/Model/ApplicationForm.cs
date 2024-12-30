using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class ApplicationForm
    {
        [Key]
        public int ID { get; set; }

        public string? name { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public string? Address { get; set; }

        public string? cv { get; set; }
    }
}