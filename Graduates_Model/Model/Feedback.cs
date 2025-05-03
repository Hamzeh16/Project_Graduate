using System.ComponentModel.DataAnnotations;

namespace Graduates_Model.Model
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        public int? Rating { get; set; } 
        [MaxLength(500)]
        public string? Message { get; set; }
        [MaxLength(50)]
        public string? CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}